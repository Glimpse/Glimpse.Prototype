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
        private readonly ISettings _settings;
        private readonly IContextData<MessageContext> _contextData;

        public GlimpseMiddleware(RequestDelegate innerNext, IServiceProvider serviceProvider)
        {
            _innerNext = innerNext;
            _runtime = new MasterRequestRuntime(serviceProvider);
            _contextData = new ContextData<MessageContext>();
        }

        public async Task Invoke(Microsoft.AspNet.Http.HttpContext context)
        {
            var newContext = new HttpContext(context, _settings);
            
            // TODO: This is the wrong place for this, AgentRuntime isn't garenteed to execute first
            _contextData.Value = new MessageContext { Id = Guid.NewGuid(), Type = "Request" };


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