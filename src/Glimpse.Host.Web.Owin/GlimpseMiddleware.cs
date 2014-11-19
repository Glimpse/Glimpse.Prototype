using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Host.Web.Owin;
using Glimpse.Agent.Web;

namespace Glimpse.Host.Web.Owin
{
    public class GlimpseMiddleware
    {
        private readonly Func<IDictionary<string, object>, Task> _innerNext;
        private readonly WebAgentRuntime _runtime;

        public GlimpseMiddleware(Func<IDictionary<string, object>, Task> innerNext)
        {
            _innerNext = innerNext;
            _runtime = new WebAgentRuntime();   // TODO: This shouldn't have this direct depedency
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var newContext = new HttpContext(environment);

            _runtime.Begin(newContext);

            await _innerNext(environment);

            _runtime.End(newContext);
        }
    }
}
