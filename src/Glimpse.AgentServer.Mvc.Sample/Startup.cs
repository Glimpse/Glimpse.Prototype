using Glimpse.Agent.AspNet.Mvc;
using Glimpse.Agent.Web;
using Glimpse.Server.Web;
using Microsoft.AspNet.Builder;
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
                    .WithMvcInspectors()
                .RunningServerWeb()
                    .WithLocalAgent();

            services.AddMvc();

            //services.AddTransient<MvcNotificationListener>();
        }

        public void Configure(IApplicationBuilder app)
        {
            //app.ApplicationServices.GetRequiredService<INotifier>().EnlistTarget(app.ApplicationServices.GetRequiredService<MvcNotificationListener>());

            app.UseGlimpseServer();
            app.UseGlimpseAgent();
            
            app.UseMvcWithDefaultRoute();
;        }
    }
}
