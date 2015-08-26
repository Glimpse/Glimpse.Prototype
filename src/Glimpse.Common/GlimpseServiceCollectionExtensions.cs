using Microsoft.Framework.DependencyInjection;

namespace Glimpse
{
    public static class GlimpseServiceCollectionExtensions
    {
        public static GlimpseServiceCollectionBuilder AddGlimpse(this IServiceCollection services)
        {
            services.TryAdd(GlimpseServices.GetDefaultServices());

            return new GlimpseServiceCollectionBuilder(services);
        } 
    }
}