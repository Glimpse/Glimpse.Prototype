using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glimpse.Server.Web
{
    public interface IQueryRequests<T>
    {
        ICollection<T> CreateFilterCollection();

        T FilterByDuration(float min = 0, float max = float.MaxValue);

        T FilterByUrl(string contains);

        T FilterByMethod(params string[] methods);

        T FilterByStatusCode(int min = 0, int max = int.MaxValue);

        Task<IEnumerable<IMessage>> Query(IEnumerable<T> filters);

        Task<IEnumerable<IMessage>> QueryWith(params T[] filters);
    }

    public abstract class QueryRequests<T> : IQueryRequests<T>
    {
        public ICollection<T> CreateFilterCollection()
        {
            return new List<T>();
        }

        public abstract T FilterByDuration(float min = 0, float max = float.MaxValue);

        public abstract T FilterByUrl(string contains);

        public abstract T FilterByMethod(params string[] methods);

        public abstract T FilterByStatusCode(int min = 0, int max = int.MaxValue);

        public abstract Task<IEnumerable<IMessage>> Query(IEnumerable<T> filters);

        public async Task<IEnumerable<IMessage>> QueryWith(params T[] filters)
        {
            return await Query(filters);
        }
    }
}