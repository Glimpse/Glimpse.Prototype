using Glimpse.Web;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace Glimpse.Server.Infrastructure
{
    public class MiddlewareResourceComposerCors : IMiddlewareResourceComposer
    { 
        public void Register(IApplicationBuilder appBuilder)
        {
            appBuilder.Use(async (context, next) =>
            {
                context.Response.Headers.Set("Access-Control-Allow-Origin", "*");

                await next();
            }); 
        }
    }
}