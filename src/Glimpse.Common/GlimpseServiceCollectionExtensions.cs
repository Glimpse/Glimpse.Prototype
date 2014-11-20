using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;

namespace Glimpse
{
    public static class GlimpseServiceCollectionExtensions
    {
        public static IServiceCollection AddGlimpse(this IServiceCollection services)
        {
            return services.Add(GlimpseServices.GetDefaultServices());
        }

        public static IServiceCollection AddGlimpse(this IServiceCollection services, IConfiguration configuration)
        {
            return services.Add(GlimpseServices.GetDefaultServices(configuration));
        }
    }
}