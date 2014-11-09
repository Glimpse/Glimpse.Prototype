using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Host.Web.Owin.Framework;

namespace Glimpse.Host.Web.Owin
{
    public class GlimpseMiddleware
    {
        private readonly Func<IDictionary<string, object>, Task> _innerNext;

        public GlimpseMiddleware(Func<IDictionary<string, object>, Task> innerNext)
        {
            _innerNext = innerNext;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var newContext = new HttpContext(environment);

            await _innerNext(environment);
        }
    }
}
