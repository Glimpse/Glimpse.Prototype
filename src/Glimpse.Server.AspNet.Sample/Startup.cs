using Glimpse.Server.Web;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse.Server.AspNet.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGlimpse()
                    .RunningServerWeb(options =>
                    {
                        //options.AllowRemote = true;
                    });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseGlimpseServer();
            app.UseGlimpseUI();
            
            app.UseWelcomePage();
        }
    }
}
