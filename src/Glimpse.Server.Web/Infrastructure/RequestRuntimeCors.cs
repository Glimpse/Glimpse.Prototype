using Glimpse.Web;
using System;
using System.Threading.Tasks;

namespace Glimpse.Server.Infrastructure
{
    public class RequestRuntimeCors : IRequestRuntime
    {
        public void Begin(IHttpContext newContext)
        {
            newContext.Response.SetHeader("Access-Control-Allow-Origin", "*");
        }

        public void End(IHttpContext newContext)
        {
        }
    }
}