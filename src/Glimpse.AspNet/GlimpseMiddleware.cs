using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System.Threading.Tasks;

namespace Glimpse.AspNet
{
    public class GlimpseMiddleware
    {
        private readonly RequestDelegate _innerNext;

        public GlimpseMiddleware(RequestDelegate innerNext)
        {
            _innerNext = innerNext;
        }

        public async Task Invoke(HttpContext context)
        {
            await _innerNext(context);
        }
    }
}