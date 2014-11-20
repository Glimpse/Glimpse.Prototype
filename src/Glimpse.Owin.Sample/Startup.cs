using Glimpse.Host.Web.Owin;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Fallback;
using Owin;
using System.Collections.Generic;

namespace Glimpse.Owin.Sample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var serviceDescriptors = new List<IServiceDescriptor>();
            var serviceProvider = serviceDescriptors.BuildServiceProvider();

            app.Use<GlimpseMiddleware>(serviceProvider);

            app.UseWelcomePage(); 
            app.UseErrorPage();
        }
    }
}