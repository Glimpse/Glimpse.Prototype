using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Glimpse.Host.Web.AspNet.Framework;

namespace Glimpse.Host.Web.AspNet
{
    public class GlimpseMiddleware
    {
        private readonly RequestDelegate _innerNext;

        public GlimpseMiddleware(RequestDelegate innerNext)
        {
            _innerNext = innerNext;
        }

        public async Task Invoke(Microsoft.AspNet.Http.HttpContext context)
        {
            var newContext = new HttpContext(context);

            await _innerNext(context);
        }
    }
}