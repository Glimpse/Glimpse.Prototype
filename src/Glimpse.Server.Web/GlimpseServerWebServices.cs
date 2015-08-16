using Glimpse.Agent;
using Microsoft.Framework.DependencyInjection;
using Glimpse.Server.Web;
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
            services.AddTransient<IRequestAuthorizerProvider, DefaultRequestAuthorizerProvider>();
            services.AddTransient<IMiddlewareResourceComposerProvider, DefaultMiddlewareResourceComposerProvider>();
            services.AddTransient<IMiddlewareLogicComposerProvider, DefaultMiddlewareLogicComposerProvider>();
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