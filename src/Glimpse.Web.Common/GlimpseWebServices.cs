using Glimpse.Web;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse
{
    public class GlimpseWebServices
    {
        public static IServiceCollection GetDefaultServices()
        {
            var services = new ServiceCollection();
             
            services.AddTransient<IMvcRazorHost, GlimpseRazorHost>(); //TODO: This probably doesn't belong here.

            return services;
        }
    }
}