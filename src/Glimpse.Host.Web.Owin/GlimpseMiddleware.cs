using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Web;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Fallback;

namespace Glimpse.Host.Web.Owin
{
    public class GlimpseMiddleware
    {
        private readonly Func<IDictionary<string, object>, Task> _innerNext;
        private readonly IServiceProvider _globalServices;
        private readonly MasterRequestRuntime _runtime;

        public GlimpseMiddleware(Func<IDictionary<string, object>, Task> innerNext, IServiceProvider globalServices)
        {
            _innerNext = innerNext;
            _globalServices = globalServices;
            _runtime = new MasterRequestRuntime(globalServices);
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            using (var localServiceScope = CreateLocalServiceScope())
            {
                var newContext = new HttpContext(environment, _globalServices, localServiceScope.ServiceProvider);

                await _runtime.Begin(newContext);

                var handler = (IRequestHandler)null;
                if (_runtime.TryGetHandle(newContext, out handler))
                {
                    await handler.Handle(newContext);
                }
                else
                {
                    await _innerNext(environment);
                }

                await _runtime.End(newContext);
            }
        }

        private IServiceScope CreateLocalServiceScope()
        {
            var globalServicesFactory = _globalServices.GetService<IServiceScopeFactory>();
            return globalServicesFactory.CreateScope();
        }
    }
}
