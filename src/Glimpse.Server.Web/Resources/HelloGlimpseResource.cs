using Glimpse.Web;
using Microsoft.AspNet.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;

namespace Glimpse.Server.Resources
{
    public class HelloGlimpseResource : IMiddlewareResourceComposer
    { 
        public void Register(IApplicationBuilder appBuilder)
        {
            appBuilder.Map("/test", chuldApp => chuldApp.Run(async context =>
            {
                var response = context.Response;

                response.Headers.Set("Content-Type", "text/plain");

                var data = Encoding.UTF8.GetBytes("Hello world, Glimpse!");
                await response.Body.WriteAsync(data, 0, data.Length);
            }));
        }
    }
}