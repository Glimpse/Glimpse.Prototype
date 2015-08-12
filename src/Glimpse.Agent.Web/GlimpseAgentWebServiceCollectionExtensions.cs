using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;
using System;
using Glimpse.Agent.Web;

namespace Glimpse
{
    public static class GlimpseAgentWebServiceCollectionExtensions
    {
        public static GlimpseAgentServiceCollectionBuilder RunningAgentWeb(this GlimpseServiceCollectionBuilder services)
        { 
            ConfigureDefaultServices(services);

            services.TryAdd(GlimpseAgentServices.GetDefaultServices());
            services.TryAdd(GlimpseWebServices.GetDefaultServices());
            services.TryAdd(GlimpseAgentWebServices.GetDefaultServices());

            return new GlimpseAgentServiceCollectionBuilder(services);
        }
         
        public static GlimpseAgentServiceCollectionBuilder ConfigureAgentWeb(this GlimpseAgentServiceCollectionBuilder services, Action<GlimpseAgentWebOptions> setupAction)
        {
            services.Configure(setupAction);

            return services;
        }

        private static void ConfigureDefaultServices(IServiceCollection services)
        {
            services.AddOptions();
        }
    }
}