using System;
using Microsoft.AspNet.Builder;

namespace Glimpse.Server.Web
{
    public static class GlimpseServerWebExtension
    {
        public static IApplicationBuilder UseGlimpseServer(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlimpseServerWebMiddleware>(app);
        }
    } 
}