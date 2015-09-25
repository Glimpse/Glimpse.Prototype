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
            var startups = app.ApplicationServices.GetRequiredService<IExtensionProvider<IAgentStartup>>();
            if (startups.Instances.Any())
            {
                var options = new StartupOptions(app);
                foreach (var startup in startups.Instances)
                {
                    startup.Run(options);
                }
            }

            return app.UseMiddleware<GlimpseAgentWebMiddleware>(app);
        }
    }
}