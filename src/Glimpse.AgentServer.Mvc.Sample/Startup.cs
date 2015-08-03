using Glimpse.Agent.AspNet.Mvc;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Notification;

namespace Glimpse.AgentServer.Mvc.Sample
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
            services.AddTransient<MvcNotificationListener>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.ApplicationServices.GetRequiredService<INotifier>().EnlistTarget(app.ApplicationServices.GetRequiredService<MvcNotificationListener>());

            app.UseGlimpse();
            app.UseGlimpseUI();

            // TODO: Nedd to find a better way of registering this. Problem is that this
            //       registration is aspnet5 specific.
            app.UseSignalR("/Glimpse/Data/Stream");

            app.UseMvcWithDefaultRoute();
;        }
    }
}
