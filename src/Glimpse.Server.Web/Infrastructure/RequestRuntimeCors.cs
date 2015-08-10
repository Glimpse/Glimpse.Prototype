using Glimpse.Web;
using Microsoft.AspNet.Http;

namespace Glimpse.Server.Infrastructure
{
    public class RequestRuntimeCors : IRequestRuntime
    {
        public void Begin(HttpContext newContext)
        {
            newContext.Response.Headers.Set("Access-Control-Allow-Origin", "*");
        }

        public void End(HttpContext newContext)
        {
        }
    }
}