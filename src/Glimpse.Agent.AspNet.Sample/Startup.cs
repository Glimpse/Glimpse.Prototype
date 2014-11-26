using Microsoft.AspNet.Builder;
using Glimpse.Host.Web.AspNet;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse.Agent.AspNet.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGlimpse()
                .RunningAgent()
                .WithRemoteHttpAgent();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseGlimpse();

            app.UseWelcomePage();
        }
    }
}
