using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;

namespace Glimpse
{
    public static class GlimpseAgentStreamServiceCollectionExtensions
    {
        public static GlimpseAgentServiceCollectionBuilder WithRemoteStreamAgent(this GlimpseAgentServiceCollectionBuilder services)
        {
            services.TryAdd(GlimpseAgentStreamServices.GetDefaultServices());

            return services;
        }
    }
}