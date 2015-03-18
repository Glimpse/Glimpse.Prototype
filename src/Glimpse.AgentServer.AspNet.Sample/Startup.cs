using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Glimpse.Host.Web.AspNet;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse.AspNet.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGlimpse()
                    .WithLocalAgent()
                    .RunningAgent()
                        .ForWeb()
                    .RunningServer();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseGlimpse();

            // TODO: Nedd to find a better way of registering this. Problem is that this
            //       registration is aspnet5 specific.
            app.UseSignalR("/Glimpse/Data/Stream");

            app.Use(next => new SamplePage().Invoke);
            /*
            app.Use(async (context, next) => {
                        var response = context.Response;

                        response.Headers.Set("Content-Type", "text/plain");

                        await response.WriteAsync("TEST!");
                    });
            */
            app.UseWelcomePage();

        }
    }
}
