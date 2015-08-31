using Glimpse.Agent.Web;
using Microsoft.AspNet.Builder;
using Glimpse.Server.Web;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse.AspNet.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGlimpse()
                .RunningAgentWeb()
                .RunningServerWeb()
                    .WithRemoteHttpClient()
                    .WithLocalAgent();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseGlimpseServer();
            app.UseGlimpseAgent();
            app.UseGlimpseUI();

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
