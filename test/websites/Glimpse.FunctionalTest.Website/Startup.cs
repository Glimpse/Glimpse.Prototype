using System.Diagnostics.Tracing;
using Glimpse.Agent.AspNet.Mvc;
using Glimpse.Agent.Web;
using Glimpse.Server.Web;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Glimpse.FunctionalTest.Website
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGlimpse()
                .RunningAgentWeb()
                .RunningServerWeb()
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
