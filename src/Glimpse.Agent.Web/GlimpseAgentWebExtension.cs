using System.Linq;
using Glimpse.Web;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse.Agent.Web
{
    public static class GlimpseAgentWebExtension
    {
        public static IApplicationBuilder UseGlimpseAgent(this IApplicationBuilder app)
        {
            var manager = app.ApplicationServices.GetRequiredService<IAgentStartupManager>();
            manager.Run(new StartupOptions(app));

            return app.UseMiddleware<GlimpseAgentWebMiddleware>(app);
        }
    }
}