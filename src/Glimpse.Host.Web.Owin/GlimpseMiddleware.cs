using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Web;
using Microsoft.Framework.DependencyInjection; 

namespace Glimpse.Host.Web.Owin
{
    public class GlimpseMiddleware
    {
        private readonly Func<IDictionary<string, object>, Task> _innerNext;
        private readonly IServiceProvider _globalServices;
        private readonly RequestRuntimeHost _runtime;
        private readonly ISettings _settings;
        private readonly IContextData<MessageContext> _contextData;

        public GlimpseMiddleware(Func<IDictionary<string, object>, Task> innerNext, IServiceProvider serviceProvider)
            : this(innerNext, serviceProvider, null)
        {
        }

        public GlimpseMiddleware(Func<IDictionary<string, object>, Task> innerNext, IServiceProvider globalServices, Func<IHttpContext, bool> shouldRun)
        {
            var typeActivator = globalServices.GetService<ITypeActivator>();
             
            _innerNext = innerNext;
            _globalServices = globalServices;
            _runtime = typeActivator.CreateInstance<RequestRuntimeHost>();
            _contextData = new ContextData<MessageContext>();

            // TODO: Need to find a way/better place for 
            var settings = new Settings();
            if (shouldRun != null) {
                settings.ShouldProfile = context => shouldRun((HttpContext)context);
            }
            _settings = settings;
        }

        // TODO: Look at pushing the workings of this into MasterRequestRuntime
        public async Task Invoke(IDictionary<string, object> environment)
        {
            using (var localServiceScope = CreateLocalServiceScope())
            {
                var newContext = new HttpContext(environment, _globalServices, localServiceScope.ServiceProvider, _settings);

                if (_runtime.Authorized(newContext))
                {
                    // TODO: This is the wrong place for this, AgentRuntime isn't garenteed to execute first
                    _contextData.Value = new MessageContext {Id = Guid.NewGuid(), Type = "Request"};

                    _runtime.Begin(newContext);

                    var handler = (IRequestHandler) null;
                    if (_runtime.TryGetHandle(newContext, out handler))
                    {
                        await handler.Handle(newContext);
                    }
                    else
                    {
                        await _innerNext(environment);
                    }

                    _runtime.End(newContext);
                }
                else
                {
                    await _innerNext(environment);
                }
            }
        }

        private IServiceScope CreateLocalServiceScope()
        {
            var globalServicesFactory = _globalServices.GetService<IServiceScopeFactory>();
            return globalServicesFactory.CreateScope();
        }
    }
}
