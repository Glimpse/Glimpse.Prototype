using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glimpse.Server.Web
{
    public class InMemoryStorage : IStorage, IQueryRequests
    {
        public const int RequestsPerPage = 50;
        public const int MaxRequests = 500; // TODO: Make this configurable

        private readonly List<IMessage> _messages;
        private readonly ConcurrentDictionary<Guid, RequestIndices> _indices;

        private static readonly object _locker = new object(); // HACK :(

        public InMemoryStorage()
        {
            _messages = new List<IMessage>();
            _indices = new ConcurrentDictionary<Guid, RequestIndices>();
        }

        public void Persist(IMessage message)
        {
            _messages.Add(message);

            if (!message.Context.Type.Equals("request", StringComparison.OrdinalIgnoreCase))
                return;

            var requestId = message.Context.Id;

            _indices.AddOrUpdate(requestId, 
                new RequestIndices(message), 
                (id, indices) =>
                {
                    indices.Update(message);
                    return indices;
                });

            // TODO: There's probably a better data structure for all of this, but not that I could find
            if (_indices.Count > MaxRequests)
            {
                lock (_locker) // Hack - I know, I know...
                {
                    var toRemove = MaxRequests/10;
                    RequestIndices removed;
                    for (int i = 0; i < toRemove; i++)
                    {
                        var idToRemove = _messages.Last().Context.Id;
                        _messages.RemoveAll(m => m.Context.Id == idToRemove);
                        _indices.TryRemove(idToRemove, out removed);
                    }
                }
            }
        }

        public Task<IEnumerable<string>> RetrieveByType(params string[] types)
        {
            if (types == null || types.Length == 0)
                throw new ArgumentException("At least one type must be specified.", nameof(types));

            return Task.Run(() => _messages.Where(m => m.Types.Intersect(types).Any()).Select(m => m.Payload));
        }

        public Task<IEnumerable<string>> GetByRequestId(Guid id)
        {
            return Task.Run(() => _messages.Where(m => m.Context.Id == id).Select(m => m.Payload));
        }

        public Task<IEnumerable<string>> Query(RequestFilters filters)
        {
            return Query(filters, null);
        }

        public Task<IEnumerable<string>> Query(RequestFilters filters, params string[] types)
        {
            if (filters == null)
                filters = RequestFilters.None;

            return Task.Run(() =>
            {
                var query = _indices.Values.AsEnumerable();

                if (filters.DurationMaximum.HasValue)
                    query = query.Where(i => i.Duration.HasValue && i.Duration <= filters.DurationMaximum);

                if (filters.DurationMinimum.HasValue)
                    query = query.Where(i => i.Duration.HasValue && i.Duration >= filters.DurationMinimum.Value);

                if (!string.IsNullOrWhiteSpace(filters.UrlContains))
                    query = query.Where(i => !string.IsNullOrWhiteSpace(i.Url) && i.Url.Contains(filters.UrlContains));

                if (filters.MethodList.Any())
                    query = query.Where(i => !string.IsNullOrWhiteSpace(i.Method) && filters.MethodList.Contains(i.Method));

                if (filters.TagList.Any())
                    query = query.Where(i => i.Tags.Intersect(filters.TagList).Any());

                if (filters.StatusCodeMinimum.HasValue)
                    query = query.Where(i => i.StatusCode.HasValue && i.StatusCode >= filters.StatusCodeMinimum);

                if (filters.StatusCodeMaximum.HasValue)
                    query = query.Where(i => i.StatusCode.HasValue && i.StatusCode <= filters.StatusCodeMaximum);

                if (filters.RequesTimeBefore.HasValue)
                    query = query.Where(i => i.DateTime.HasValue && i.DateTime < filters.RequesTimeBefore);

                return query
                    .OrderByDescending(i => i.DateTime)
                    .Take(RequestsPerPage)
                    .Join(
                        types == null ? _messages : _messages.Where(m => m.Types.Intersect(types).Any()), // only filter by type if types are specified
                        i => i.Id, 
                        m => m.Context.Id, 
                        (i, m) => m.Payload);
                });
        }
    }
}