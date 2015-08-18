using Glimpse.Server.Web;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse.Server.AspNet.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGlimpse()
                .RunningServerWeb()
                    .WithRemoteStreamAgent();
        }

        public void Configure(IApplicationBuilder app)
        {
            // TODO: Nedd to find a better way of registering this. Problem is that this
            //       registration is aspnet5 specific.
            app.UseSignalR("/Glimpse/Data/Stream");

            app.UseGlimpseServer();
            app.UseGlimpseUI();
            
            app.UseWelcomePage();
        }
    }
}
