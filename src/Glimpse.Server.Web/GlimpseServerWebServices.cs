using Glimpse.Agent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Framework.OptionsModel;
using Glimpse.Server.Web;
using Glimpse.Web;
using Microsoft.Extensions.OptionsModel;

namespace Glimpse
{
    public class GlimpseServerWebServices
    {
        public static IServiceCollection GetDefaultServices()
        {
            var services = new ServiceCollection();

            //
            // Common
            //
            services.AddSingleton<IServerBroker, DefaultServerBroker>();
            services.AddSingleton<IStorage, InMemoryStorage>();
            services.AddTransient<IResourceManager, ResourceManager>();

            //
            // Options
            //
            services.AddTransient<IConfigureOptions<GlimpseServerWebOptions>, GlimpseServerWebOptionsSetup>();
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
            services.AddSingleton<IMessagePublisher, InProcessChannel>();

            return services;
        }
    }
}