using Owin;
using System;

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