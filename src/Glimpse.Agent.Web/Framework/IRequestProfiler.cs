using Microsoft.AspNet.Http;

namespace Glimpse.Agent.Web
{
    public interface IRequestProfiler
    {
        void Begin(HttpContext newContext);

        void End(HttpContext newContext);
    }
}