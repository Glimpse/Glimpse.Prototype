using Glimpse.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Glimpse
{
    public static class GlimpseSystemWebServiceCollectionExtensions
    {
        public static GlimpseServiceCollectionBuilder AddGlimpse(this IServiceCollection services)
        {
            services.TryAdd(CommonSystemWebServices.GetDefaultServices());

            return services.AddGlimpseCore();
        }

        public static GlimpseServiceCollectionBuilder AddGlimpse(this IServiceCollection services, bool autoRegisterComponents)
        {
            services.TryAdd(CommonSystemWebServices.GetDefaultServices());

            return services.AddGlimpseCore(autoRegisterComponents);
        } 
    }
}