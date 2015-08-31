using Glimpse.Agent.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse
{
    public class GlimpseAgentWebMvcServices
    {
        public static IServiceCollection GetDefaultServices()
        {
            var services = new ServiceCollection();

            //
            // Options
            //
            services.AddTransient<IMvcRazorHost, GlimpseRazorHost>();

            return services;
        }
    }
}