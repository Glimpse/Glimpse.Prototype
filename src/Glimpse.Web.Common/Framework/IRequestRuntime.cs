using Microsoft.AspNet.Http;

namespace Glimpse.Web
{
    public interface IRequestRuntime
    {
        void Begin(HttpContext newContext);

        void End(HttpContext newContext);
    }
}