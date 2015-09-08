using System.Diagnostics.Tracing;
using Glimpse.Agent.AspNet.Mvc;
using Glimpse.Agent.Web;
using Glimpse.Server.Web;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

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
            services.AddTransient<MvcTelemetryListener>();
        }

        public void Configure(IApplicationBuilder app)
        {
            var telemetryListener = app.ApplicationServices.GetRequiredService<TelemetryListener>();
            telemetryListener.SubscribeWithAdapter(app.ApplicationServices.GetRequiredService<MvcTelemetryListener>());

            app.UseGlimpseServer();
            app.UseGlimpseAgent();

            app.UseMvcWithDefaultRoute();
        }
    }
}
