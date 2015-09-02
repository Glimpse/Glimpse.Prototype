using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Glimpse.Agent.Web
{
    public interface IInspector
    {
        void Before(HttpContext context);

        void After(HttpContext context);
    }
}
