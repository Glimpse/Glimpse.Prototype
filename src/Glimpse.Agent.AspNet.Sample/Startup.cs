using Glimpse.Agent.Web;
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
                    .ForWeb()
                        .Configure<GlimpseAgentWebOptions>(options =>
                        {
                            //options.IgnoredStatusCodes.Add(200);
                        })
                .WithRemoteStreamAgent();
                //.WithRemoteHttpAgent();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseGlimpse();

            app.UseWelcomePage();
        }
    }
}
