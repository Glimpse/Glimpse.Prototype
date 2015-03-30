using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;

namespace Glimpse
{
    public static class GlimpseServerWebServiceCollectionExtensions
    {
        public static GlimpseServerServiceCollectionBuilder RunningServerWeb(this GlimpseServiceCollectionBuilder services)
        {   
            ConfigureDefaultServices(services);

            services.TryAdd(GlimpseWebServices.GetDefaultServices());
            services.TryAdd(GlimpseServerServices.GetDefaultServices());
            services.TryAdd(GlimpseServerWebServices.GetDefaultServices()); 
            UseSignalR(services);

            return new GlimpseServerServiceCollectionBuilder(services);
        }

        public static IServiceCollection WithLocalAgent(this GlimpseServerServiceCollectionBuilder services)
        { 
            return services.Add(GlimpseServerWebServices.GetLocalAgentServices());
        }

        // TODO: Confirm that this is where this should be registered
        private static void UseSignalR(IServiceCollection services)
        {
            // TODO: Config isn't currently being handled - https://github.com/aspnet/SignalR-Server/issues/51
            services.AddSignalR(options =>
                {
                    options.Hubs.EnableDetailedErrors = true;
                });
        }

        private static void ConfigureDefaultServices(IServiceCollection services)
        {
            services.AddOptions();
        }
    }
}