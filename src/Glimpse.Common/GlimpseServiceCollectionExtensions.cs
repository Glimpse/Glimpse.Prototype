using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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