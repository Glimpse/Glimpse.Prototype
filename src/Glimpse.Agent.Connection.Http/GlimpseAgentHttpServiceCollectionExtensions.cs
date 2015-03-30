using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;

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