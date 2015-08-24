using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glimpse.Server.Web
{
    public class InMemoryStorage : IQueryRequests<Func<RequestIndices, bool>>, IStorage
    {
        private readonly IList<IMessage> _store;
        private readonly IDictionary<Guid, RequestIndices> _indices;

        public InMemoryStorage()
        {
            _store = new List<IMessage>();
            _indices = new Dictionary<Guid, RequestIndices>();
        }

        public void Persist(IMessage message)
        {
            _store.Add(message);

            if (!message.Context.Type.Equals("request")) // TODO: Should this be case insensitive?
                return;

            var requestId = message.Context.Id;

            if (_indices.ContainsKey(requestId))
            {
                _indices[requestId].Update(message);
            }
            else
            {
                _indices.Add(requestId, new RequestIndices(message));
            }
        }

        public Task<IEnumerable<IMessage>> RetrieveByType(params string[] types)
        {
            if (types == null || types.Length == 0)
                throw new ArgumentException("At least one type must be specified.", "types");

            return Task.Run(() => _store.Where(m => m.Types.Intersect(types).Any()));
        }

        public ICollection<Func<RequestIndices, bool>> CreateFilterCollection()
        {
            return new List<Func<RequestIndices, bool>>();
        }

        public Task<IEnumerable<IMessage>> GetByRequestId(Guid id)
        {
            return Task.Run(() => _store.Where(m => m.Context.Id == id));
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

                return query.Join(
                    types == null ? _store : _store.Where(m => m.Types.Intersect(types).Any()), // only filter by type if types are specified
                    i => i.Id, 
                    m => m.Context.Id, 
                    (i, m) => m);
            });
        }
    }
}