using Microsoft.Framework.DependencyInjection;

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