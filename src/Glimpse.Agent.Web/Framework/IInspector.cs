using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Glimpse.Agent.Web
{
    public interface IInspector
    {
        Task Before(HttpContext context);

        Task After(HttpContext context);
    }
}
