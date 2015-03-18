using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;

namespace Glimpse
{
    public static class GlimpseAgentWebServiceCollectionExtensions
    {
        public static IServiceCollection ForWeb(this IServiceCollection services)
        {
            return services.Add(GlimpseAgentWebServices.GetDefaultServices());
        }

        public static IServiceCollection ForWeb(this IServiceCollection services, IConfiguration configuration)
        {
            return services.Add(GlimpseAgentWebServices.GetDefaultServices(configuration));
        }
    }
}