using Glimpse.Agent.Razor;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace Glimpse
{
    public class GlimpseAgentMvcServices
    {
        public static IServiceCollection GetDefaultServices()
        {
            var services = new ServiceCollection();

            //
            // Options
            //
            services.AddTransient<IMvcRazorHost, ScriptInjectorRazorHost>();

            return services;
        }
    }
}