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
        private readonly IServiceProvider _serviceProvider;
        private readonly MasterRequestRuntime _runtime;

        public GlimpseMiddleware(Func<IDictionary<string, object>, Task> innerNext, IServiceProvider serviceProvider)
        {
            _innerNext = innerNext;
            _serviceProvider = serviceProvider;
            _runtime = new MasterRequestRuntime();
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var newContext = new HttpContext(environment, _serviceProvider);

            _runtime.Begin(newContext);

            await _innerNext(environment);

            _runtime.End(newContext);
        }
    }
}
