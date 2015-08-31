using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glimpse.Server.Web
{
    public class InMemoryStorage : IQueryRequests<Func<RequestIndices, bool>>, IStorage
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

        public Task<IEnumerable<IMessage>> RetrieveByType(params string[] types)
        {
            if (types == null || types.Length == 0)
                throw new ArgumentException("At least one type must be specified.", "types");

            return Task.Run(() => _messages.Where(m => m.Types.Intersect(types).Any()));
        }

        public ICollection<Func<RequestIndices, bool>> CreateFilterCollection()
        {
            return new List<Func<RequestIndices, bool>>();
        }

        public Task<IEnumerable<IMessage>> GetByRequestId(Guid id)
        {
            return Task.Run(() => _messages.Where(m => m.Context.Id == id));
        }

        public Func<RequestIndices, bool> FilterByDuration(float min = 0, float max = float.MaxValue)
        {
            return i => i.Duration.HasValue && i.Duration.Value >= min && i.Duration.Value <= max;
        }

        public Func<RequestIndices, bool> FilterByUrl(string contains)
        {
            return i => !string.IsNullOrWhiteSpace(i.Url) && i.Url.Contains(contains);
        }

        public Func<RequestIndices, bool> FilterByMethod(params string[] methods)
        {
            return i => !string.IsNullOrWhiteSpace(i.Method) && methods.Contains(i.Method);
        }

        public Func<RequestIndices, bool> FilterByTag(params string[] tags)
        {
            return i => i.Tags.Intersect(tags).Any();
        }

        public Func<RequestIndices, bool> FilterByStatusCode(int min = 0, int max = int.MaxValue)
        {
            return i => i.StatusCode.HasValue && i.StatusCode.Value >= min && i.StatusCode.Value <= max;
        }

        public Func<RequestIndices, bool> FilterByDateTime(DateTime before)
        {
            return i => i.DateTime.HasValue && i.DateTime.Value < before;
        }

        public Task<IEnumerable<IMessage>> Query(params Func<RequestIndices, bool>[] filters)
        {
            return Query(filters, null);
        }

        public Task<IEnumerable<IMessage>> Query(IEnumerable<Func<RequestIndices, bool>> filters, params string[] types)
        {
            return Task.Run(() =>
            {
                var query = _indices.Values.AsEnumerable();

                foreach (var filter in filters)
                    query = query.Where(filter);

                return query
                    .OrderByDescending(i => i.DateTime)
                    .Take(RequestsPerPage)
                    .Join(
                        types == null ? _messages : _messages.Where(m => m.Types.Intersect(types).Any()), // only filter by type if types are specified
                        i => i.Id, 
                        m => m.Context.Id, 
                        (i, m) => m);
                });
        }
    }
}