using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;

namespace Glimpse
{
    public static class GlimpseAgentWebServiceCollectionExtensions
    {
        public static IServiceCollection ForWeb(this IServiceCollection services)
        {
            return ForWeb(services, null);
        }

        public static IServiceCollection ForWeb(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDefaultServices(services, configuration);
            return services.Add(GlimpseAgentWebServices.GetDefaultServices(configuration));
        }

        private static void ConfigureDefaultServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions(configuration);
        }
    }
}