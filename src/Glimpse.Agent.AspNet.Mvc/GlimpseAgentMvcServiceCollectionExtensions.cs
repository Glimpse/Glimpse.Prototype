using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Glimpse
{
    public static class GlimpseAgentMvcServiceCollectionExtensions
    {
        public static GlimpseAgentServiceCollectionBuilder WithMvcInspectors(this GlimpseAgentServiceCollectionBuilder services)
        {
            services.TryAdd(GlimpseAgentMvcServices.GetDefaultServices());

            return services;
        }
    }
}