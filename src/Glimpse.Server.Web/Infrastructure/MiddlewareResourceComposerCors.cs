using Microsoft.AspNet.Builder;

namespace Glimpse.Server.Web
{
    public class MiddlewareResourceComposerCors : IMiddlewareLogicComposer
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