using Microsoft.AspNet.Builder;

namespace Glimpse.Server
{
    public static class GlimpseServerExtension
    {
        public static IApplicationBuilder UseGlimpseServer(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlimpseServerMiddleware>(app);
        }
    } 
}