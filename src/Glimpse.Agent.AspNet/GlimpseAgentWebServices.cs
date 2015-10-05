using Glimpse.Agent;
using Glimpse.Agent.AspNet;
using Glimpse.Initialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;

namespace Glimpse
{
    public class GlimpseAgentWebServices
    {
        public static IServiceCollection GetDefaultServices()
        {
            var services = new ServiceCollection();

            //
            // Broker
            //
            services.AddSingleton<IAgentBroker, DefaultAgentBroker>();
            services.AddTransient<IMessagePublisher, HttpMessagePublisher>();

            //
            // Options
            //
            services.AddTransient<IConfigureOptions<GlimpseAgentWebOptions>, GlimpseAgentWebOptionsSetup>();
            services.AddSingleton<IRequestIgnorerUriProvider, DefaultRequestIgnorerUriProvider>();
            services.AddSingleton<IRequestIgnorerStatusCodeProvider, DefaultRequestIgnorerStatusCodeProvider>();
            services.AddSingleton<IRequestIgnorerContentTypeProvider, DefaultRequestIgnorerContentTypeProvider>();
            services.AddSingleton<IExtensionProvider<IRequestIgnorer>, DefaultExtensionProvider<IRequestIgnorer>>();
            services.AddSingleton<IExtensionProvider<IInspectorFunction>, DefaultExtensionProvider<IInspectorFunction>>();
            services.AddSingleton<IExtensionProvider<IInspector>, DefaultExtensionProvider<IInspector>>();
            services.AddSingleton<IExtensionProvider<IAgentStartup>, DefaultExtensionProvider<IAgentStartup>>();
            
            //
            // Common
            //
            services.AddTransient<IAgentStartupManager, DefaultAgentStartupManager>();
            services.AddTransient<IRequestIgnorerManager, DefaultRequestIgnorerManager>();
            services.AddTransient<IInspectorFunctionManager, DefaultInspectorFunctionManager>();
            services.AddTransient<WebTelemetryListener>();

            return services;
        }
    }
}