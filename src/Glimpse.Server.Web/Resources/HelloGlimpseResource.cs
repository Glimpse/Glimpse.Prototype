using Glimpse.Web;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Server.Resources
{
    public class HelloGlimpseResource : IRequestHandler
    {
        public bool WillHandle(IHttpContext context)
        {
            return context.Request.Path == "/Glimpse";
        }

        public async Task Handle(IHttpContext context)
        {
            var response = context.Response;

            response.SetHeader("Content-Type", "text/plain");

            var data = Encoding.UTF8.GetBytes("Hello world, Glimpse!");
            await response.WriteAsync(data);
        }
    }
}