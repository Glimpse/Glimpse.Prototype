using Glimpse.Initialization;
using Glimpse.Server;
using Glimpse.Server.Configuration;
using Glimpse.Server.Internal;
using Glimpse.Server.Resources;
using Glimpse.Server.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;

namespace Glimpse
{
    public class GlimpseServerServices
    {
        public static IServiceCollection GetDefaultServices()
        {
            var services = new ServiceCollection();

            //
            // Common
            //
            services.AddSingleton<IServerBroker, DefaultServerBroker>();
            services.AddSingleton<IStorage, InMemoryStorage>();
            services.AddSingleton<IResourceManager, ResourceManager>();

            //
            // Options
            //
            services.AddTransient<IConfigureOptions<GlimpseServerOptions>, GlimpseServerOptionsSetup>();
            services.AddTransient<IExtensionProvider<IAllowClientAccess>, DefaultExtensionProvider<IAllowClientAccess>>();
            services.AddTransient<IExtensionProvider<IAllowAgentAccess>, DefaultExtensionProvider<IAllowAgentAccess>>();
            services.AddTransient<IExtensionProvider<IResource>, DefaultExtensionProvider<IResource>>();
            services.AddTransient<IExtensionProvider<IResourceStartup>, DefaultExtensionProvider<IResourceStartup>>();
            services.AddSingleton<IAllowRemoteProvider, DefaultAllowRemoteProvider>();
            services.AddSingleton<IScriptOptionsProvider, DefaultScriptOptionsProvider>();
            services.AddSingleton<IMetadataProvider, DefaultMetadataProvider>();

            return services;
        }

        public static IServiceCollection GetLocalAgentServices()
        {
            var services = new ServiceCollection();

            //
            // Broker
            //
            services.AddSingleton<IMessagePublisher, InProcessPublisher>();

            return services;
        }
    }
}