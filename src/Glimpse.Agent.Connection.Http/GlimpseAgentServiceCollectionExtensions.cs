using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;

namespace Glimpse
{
    public static class GlimpseAgentServiceCollectionExtensions
    {
        public static IServiceCollection WithRemoteHttpAgent(this IServiceCollection services)
        {
            return services.Add(GlimpseAgentServices.GetDefaultServices());
        }

        public static IServiceCollection WithRemoteHttpAgent(this IServiceCollection services, IConfiguration configuration)
        {
            return services.Add(GlimpseAgentServices.GetDefaultServices(configuration));
        }
    }
}