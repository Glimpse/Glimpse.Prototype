using System;
using Microsoft.AspNet.Builder;
using Glimpse.Host.Web.AspNet;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse.Server.AspNet.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGlimpse()
                    .RunningServerWeb();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseGlimpse();
            app.UseGlimpseUI();

            // TODO: Nedd to find a better way of registering this. Problem is that this
            //       registration is aspnet5 specific.
            app.UseSignalR("/Glimpse/Data/Stream");

            app.UseWelcomePage();
        }
    }
}
