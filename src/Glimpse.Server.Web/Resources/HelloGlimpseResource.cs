using Glimpse.Web;
using Microsoft.AspNet.Http;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Server.Resources
{
    public class HelloGlimpseResource : IRequestHandler
    {
        public bool WillHandle(HttpContext context)
        {
            return context.Request.Path == "/Glimpse";
        }

        public async Task Handle(HttpContext context)
        {
            var response = context.Response;

            response.Headers.Set("Content-Type", "text/plain");

            var data = Encoding.UTF8.GetBytes("Hello world, Glimpse!");
            await response.Body.WriteAsync(data, 0, data.Length);
        }
    }
}