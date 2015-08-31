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

            //
            // Store
            //
            services.AddSingleton<IStorage, InMemoryStorage>();

            //
            // Options
            //
            services.AddTransient<IConfigureOptions<GlimpseServerWebOptions>, GlimpseServerWebOptionsSetup>();
            services.AddTransient<IRequestAuthorizerProvider, DefaultRequestAuthorizerProvider>();
            services.AddTransient<IResourceProvider, DefaultResourceProvider>();
            services.AddTransient<IResourceStartupProvider, DefaultResourceStartupProvider>();
            services.AddSingleton<IAllowRemoteProvider, DefaultAllowRemoteProvider>();
            services.AddTransient<IResourceManager, ResourceManager>();

            return services;
        }

        public static IServiceCollection GetLocalAgentServices()
        {
            var services = new ServiceCollection();

            //
            // Broker
            //
            services.AddSingleton<IMessagePublisher, InProcessChannel>();

            return services;
        }
    }
}