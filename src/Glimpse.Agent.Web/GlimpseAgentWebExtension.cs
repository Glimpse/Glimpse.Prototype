using System;
using Microsoft.AspNet.Builder;

namespace Glimpse.Agent.Web
{
    public static class GlimpseAgentWebExtension
    {
        public static IApplicationBuilder UseGlimpseAgent(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlimpseAgentWebMiddleware>(app);
        }
    }
}