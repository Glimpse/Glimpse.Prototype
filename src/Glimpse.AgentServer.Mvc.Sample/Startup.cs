using System.Diagnostics.Tracing;
using Glimpse.Agent.AspNet.Mvc;
using Glimpse.Agent.Web;
using Glimpse.Server.Web;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse.AgentServer.Mvc.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGlimpse()
                .RunningAgentWeb()
                    .WithMvcInspectors()
                .RunningServerWeb(settings => settings.AllowRemote = true) // Temp workaround for kestrel not implementing IHttpConnectionFeature
                    .WithLocalAgent();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseGlimpseServer();
            app.UseGlimpseAgent();
            
            app.UseMvcWithDefaultRoute();
        }
    }
}
