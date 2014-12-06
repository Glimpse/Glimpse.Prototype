using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Glimpse.Web;
using System;

namespace Glimpse.Host.Web.AspNet
{
    public class GlimpseMiddleware
    {
        private readonly RequestDelegate _innerNext;
        private readonly MasterRequestRuntime _runtime;

        public GlimpseMiddleware(RequestDelegate innerNext, IServiceProvider serviceProvider)
        {
            _innerNext = innerNext;
            _runtime = new MasterRequestRuntime(serviceProvider);
        }

        public async Task Invoke(Microsoft.AspNet.Http.HttpContext context)
        {
            var newContext = new HttpContext(context);

            await _runtime.Begin(newContext);

            var handler = (IRequestHandler)null;
            if (_runtime.TryGetHandle(newContext, out handler))
            {
                await handler.Handle(newContext);
            }
            else
            {
                await _innerNext(context);
            }

            // TODO: This doesn't work correctly :(
            await _runtime.End(newContext);
        }
    }
}