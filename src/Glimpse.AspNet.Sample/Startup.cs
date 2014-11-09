using Microsoft.AspNet.Builder;
using Glimpse.Host.Web.AspNet;

namespace Glimpse.AspNet.Sample
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseGlimpse();
        }
    }
}
