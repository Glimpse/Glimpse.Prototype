using Microsoft.Framework.DependencyInjection.Extensions;

namespace Glimpse
{
    public static class GlimpseAgentHttpServiceCollectionExtensions
    {
        public static GlimpseAgentServiceCollectionBuilder WithRemoteHttpAgent(this GlimpseAgentServiceCollectionBuilder services)
        {  
            services.TryAdd(GlimpseAgentHttpServices.GetDefaultServices()); 

            return services;
        }
    }
}