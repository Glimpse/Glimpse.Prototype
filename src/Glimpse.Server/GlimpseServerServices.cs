using Glimpse.Initialization;
using Glimpse.Server;
using Glimpse.Server.Configuration;
using Glimpse.Server.Internal;
using Glimpse.Server.Resources;
using Glimpse.Server.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.OptionsModel;
using System.Linq;

namespace Glimpse
{
    public static class GlimpseServerServices
    {
        public static IServiceCollection AddServerServices(this IServiceCollection services)
        {
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
            services.AddSingleton<IMetadataProvider, DefaultMetadataProvider>();

            if (services.Any(s => s.ServiceType == typeof (IScriptOptionsProvider)))
            {
                services.Replace(new ServiceDescriptor(
                    typeof (IScriptOptionsProvider),
                    typeof (DefaultScriptOptionsProvider),
                    ServiceLifetime.Singleton));
            }
            else
            {
                services.AddSingleton<IScriptOptionsProvider, DefaultScriptOptionsProvider>();
            }

            return services;
        }

        public static IServiceCollection AddLocalAgentServices(this IServiceCollection services)
        {
            //
            // Broker
            //
            services.AddSingleton<IMessagePublisher, InProcessPublisher>();

            return services;
        }
    }
}