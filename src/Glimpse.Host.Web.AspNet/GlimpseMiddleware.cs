using System.Threading.Tasks;
using Microsoft.AspNet.Builder; 
using Glimpse.Web;

namespace Glimpse.Host.Web.AspNet
{
    public class GlimpseMiddleware
    {
        private readonly RequestDelegate _innerNext;
        private readonly RequestRuntime _runtime;

        public GlimpseMiddleware(RequestDelegate innerNext)
        {
            _innerNext = innerNext;
            _runtime = new RequestRuntime();
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