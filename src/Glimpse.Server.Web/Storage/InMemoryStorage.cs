using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glimpse.Server.Web
{
    public class InMemoryStorage : QueryRequests<Func<RequestIndices, bool>>, IStorage
    {
        private readonly IList<IMessage> _store;
        private readonly IList<RequestIndices> _indices;

        public InMemoryStorage()
        {
            _store = new List<IMessage>();
            _indices = new List<RequestIndices>();
        }

        public void Persist(IMessage message)
        {
            _indices.Add(new RequestIndices(message));
            _store.Add(message);
        }

        public Task<IEnumerable<IMessage>> RetrieveBy(Guid id)
        {
            return Task.Run(() => _store.Where(m => m.Context.Id == id));
        }

        public override Func<RequestIndices, bool> FilterByDuration(float min = 0, float max = float.MaxValue)
        {
            return i => i.Duration.HasValue && i.Duration.Value >= min && i.Duration.Value <= max;
        }

        public override Func<RequestIndices, bool> FilterByUrl(string contains)
        {
            return i => !string.IsNullOrWhiteSpace(i.Url) && i.Url.Contains(contains);
        }

        public override Func<RequestIndices, bool> FilterByMethod(params string[] methods)
        {
            return i => !string.IsNullOrWhiteSpace(i.Method) && methods.Contains(i.Method);
        }

        public override Func<RequestIndices, bool> FilterByStatusCode(int min = 0, int max = int.MaxValue)
        {
            return i => i.StatusCode.HasValue && i.StatusCode.Value >= min && i.StatusCode.Value <= max;
        }

        public override Task<IEnumerable<IMessage>> Query(IEnumerable<Func<RequestIndices, bool>> filters)
        {
            return Task.Factory.StartNew<IEnumerable<IMessage>>(() =>
            {
                /*
                var query = _indices.AsQueryable();

                foreach (var filter in filters) query.Where(filter);

                return query.Join(_store, i => i.Id, m => m.Context.Id, (i, m) => m);
                */
                return null;
            });
        }

        public IEnumerable<IMessage> AllMessages
        {
            get { return _store; }
        }
    }

    public struct RequestIndices
    {
        private readonly float? _duration;
        private readonly string _url;
        private readonly string _method;
        private readonly int? _statusCode;
        private readonly Guid _id;

        public RequestIndices(IMessage message)
        {
            _id = message.Context.Id;

            var indices = message.Indices;
            _duration = indices?.GetValueOrDefault("request.duration") as float?;
            _statusCode = indices?.GetValueOrDefault("request.statuscode") as int?;
            _url = indices?.GetValueOrDefault("request.url") as string;
            _method = indices?.GetValueOrDefault("request.method") as string;
            _statusCode = indices?.GetValueOrDefault("request.statuscode") as int?;
        }

        public float? Duration
        {
            get { return _duration; }
        }

        public string Url
        {
            get{ return _url; }
        }

        public string Method
        {
            get { return _method; }
        }
        public int? StatusCode
        {
            get { return _statusCode; }
        }

        public Guid Id
        {
            get { return _id; }
        }
    }
}