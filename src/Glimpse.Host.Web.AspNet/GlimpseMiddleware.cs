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
            : this(innerNext, serviceProvider, null)
        {
        }

        public GlimpseMiddleware(RequestDelegate innerNext, IServiceProvider serviceProvider, Func<IHttpContext, bool> shouldRun)
        {
            _innerNext = innerNext;
            _runtime = new MasterRequestRuntime(serviceProvider);
            _contextData = new ContextData<MessageContext>();

            // TODO: Need to find a way/better place for 
            var settings = new Settings();
            if (shouldRun != null)
            {
                settings.ShouldProfile = context => shouldRun((HttpContext)context);
            }
            _settings = settings;
        }

        // TODO: Look at pushing the workings of this into MasterRequestRuntime
        public async Task Invoke(Microsoft.AspNet.Http.HttpContext context)
        {
            var newContext = new HttpContext(context, _settings);

            if (_runtime.Authorized(newContext))
            {
                // TODO: This is the wrong place for this, AgentRuntime isn't garenteed to execute first
                _contextData.Value = new MessageContext {Id = Guid.NewGuid(), Type = "Request"};

                await _runtime.Begin(newContext);

                var handler = (IRequestHandler) null;
                if (_runtime.TryGetHandle(newContext, out handler))
                {
                    await handler.Handle(newContext);
                }
                else
                {
                    await _innerNext(context);
                }

                // TODO: This doesn't work correctly :( (headers)
                await _runtime.End(newContext);
            }
            else
            {
                await _innerNext(context);
            }
        }
    }
}