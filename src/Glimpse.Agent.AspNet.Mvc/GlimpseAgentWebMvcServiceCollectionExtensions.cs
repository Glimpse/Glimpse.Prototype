using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Glimpse
{
    public static class GlimpseAgentWebMvcServiceCollectionExtensions
    {
        public static GlimpseAgentServiceCollectionBuilder WithMvcInspectors(this GlimpseAgentServiceCollectionBuilder services)
        {
            services.TryAdd(GlimpseAgentWebMvcServices.GetDefaultServices());

            return services;
        }
    }
}