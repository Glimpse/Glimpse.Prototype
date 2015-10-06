using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Glimpse.Agent;

namespace Glimpse
{
    public static class GlimpseAgentWebServiceCollectionExtensions
    {
        public static GlimpseAgentServiceCollectionBuilder RunningAgentWeb(this GlimpseServiceCollectionBuilder services)
        {
            return services.RunningAgentWeb(null);
        }
         
        public static GlimpseAgentServiceCollectionBuilder RunningAgentWeb(this GlimpseServiceCollectionBuilder services, Action<GlimpseAgentWebOptions> setupAction)
        {
            services.AddOptions();
            
            services.TryAdd(GlimpseAgentWebServices.GetDefaultServices());

            if (setupAction != null)
            {
                services.Configure(setupAction);
            }

            return new GlimpseAgentServiceCollectionBuilder(services);
        }
    }
}