using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;

namespace Glimpse
{
    public static class GlimpseServerServiceCollectionExtensions
    {
        public static IServiceCollection RunningServer(this IServiceCollection services)
        {
            UseSignalR(services, null);

            return services.Add(GlimpseServerServices.GetDefaultServices());
        }

        public static IServiceCollection RunningServer(this IServiceCollection services, IConfiguration configuration)
        {
            UseSignalR(services, configuration);

            return services.Add(GlimpseServerServices.GetDefaultServices(configuration));
        }

        public static IServiceCollection WithLocalAgent(this IServiceCollection services)
        {
            return services.Add(GlimpseServerServices.GetPublisherServices());
        }

        public static IServiceCollection WithLocalAgent(this IServiceCollection services, IConfiguration configuration)
        {
            return services.Add(GlimpseServerServices.GetPublisherServices(configuration));
        }

        // TODO: Confirm that this is where this should be registered
        private static void UseSignalR(IServiceCollection services, IConfiguration configuration)
        {
            // TODO: Config isn't currently being handled - https://github.com/aspnet/SignalR-Server/issues/51
            services.AddSignalR(options =>
                {
                    options.Hubs.EnableDetailedErrors = true;
                });
        }
    }
}