using Glimpse.Agent;
using Glimpse.Server;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System.Collections.Generic;
using Glimpse.Server.Options;
using Microsoft.Framework.OptionsModel;

namespace Glimpse
{
    public class GlimpseServerWebServices
    {
        public static IServiceCollection GetDefaultServices()
        {
            var services = new ServiceCollection();

            //
            // Broker
            //
            services.AddSingleton<IServerBroker, DefaultServerBroker>();
            services.AddSingleton<IClientBroker, DefaultClientBroker>();

            //
            // Store
            //
            services.AddSingleton<IStorage, InMemoryStorage>();

            //
            // Options
            //
            services.AddTransient<IConfigureOptions<GlimpseServerWebOptions>, GlimpseServerWebOptionsSetup>();
            services.AddSingleton<IAllowRemoteProvider, DefaultAllowRemoteProvider>();

            return services;
        }

        public static IServiceCollection GetLocalAgentServices()
        {
            var services = new ServiceCollection();

            //
            // Broker
            //
            services.AddSingleton<IChannelSender, InProcessChannel>();

            return services;
        }
    }
}