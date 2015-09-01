using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glimpse.Server.Web
{
    // URI Template for this interface: {?min,max,url,methods,statuscodes,tags,before}
    public interface IQueryRequests<T>
    {
        ICollection<T> CreateFilterCollection();

        Task<IEnumerable<string>> GetByRequestId(Guid id);

        T FilterByDuration(float min = 0, float max = float.MaxValue);

        T FilterByUrl(string contains);

        T FilterByMethod(params string[] methods);

        T FilterByStatusCode(int min = 0, int max = int.MaxValue);

        T FilterByTag(params string[] tags);

        T FilterByDateTime(DateTime before);

        Task<IEnumerable<string>> Query(params T[] filters);

        Task<IEnumerable<string>> Query(IEnumerable<T> filters, params string[] types);
    }
}