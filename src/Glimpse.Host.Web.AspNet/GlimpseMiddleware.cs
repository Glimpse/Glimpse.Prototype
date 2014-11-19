using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Glimpse.Host.Web.AspNet;
using Glimpse.Agent.Web;

namespace Glimpse.Host.Web.AspNet
{
    public class GlimpseMiddleware
    {
        private readonly RequestDelegate _innerNext;
        private readonly WebAgentRuntime _runtime;

        public GlimpseMiddleware(RequestDelegate innerNext)
        {
            _innerNext = innerNext;
            _runtime = new WebAgentRuntime();   // TODO: This shouldn't have this direct depedency
        }

        public async Task Invoke(Microsoft.AspNet.Http.HttpContext context)
        {
            var newContext = new HttpContext(context);

            _runtime.Begin(newContext);

            await _innerNext(context);

            _runtime.End(newContext);
        }
    }
}