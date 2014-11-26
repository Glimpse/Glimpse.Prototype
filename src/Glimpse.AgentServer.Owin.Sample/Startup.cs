using Glimpse.Host.Web.Owin;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Fallback;
using Owin;

namespace Glimpse.AgentServer.Owin.Sample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddGlimpse()
                    .WithLocalAgent()
                    .RunningServer();

            var serviceProvider = serviceDescriptors.BuildServiceProvider();

            app.Use<GlimpseMiddleware>(serviceProvider);

            app.UseWelcomePage(); 
            app.UseErrorPage();
        }
    }
}