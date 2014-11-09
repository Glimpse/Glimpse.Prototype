using Glimpse.Host.Web.Owin;
using Owin;

namespace Glimpse.Owin.Sample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use<GlimpseMiddleware>();
            app.UseWelcomePage(); 
            app.UseErrorPage();
        }
    }
}