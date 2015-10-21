using Glimpse.Agent;
using Glimpse.Agent.Internal.Messaging;
using Glimpse.Agent.Configuration;
using Glimpse.Agent.Inspectors;
using Glimpse.Agent.Internal.Inspectors.Mvc;
using Glimpse.Initialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;
using System.Linq;

namespace Glimpse
{
    public static class GlimpseAgentServices
    {
        public static IServiceCollection AddAgentServices(this IServiceCollection services)
        {
            //
            // Broker
            //
            services.AddSingleton<IAgentBroker, DefaultAgentBroker>();
            services.AddTransient<IMessagePublisher, HttpMessagePublisher>();

            //
            // Options
            //
            services.AddTransient<IConfigureOptions<GlimpseAgentOptions>, GlimpseAgentOptionsSetup>();
            services.AddSingleton<IRequestIgnorerUriProvider, DefaultRequestIgnorerUriProvider>();
            services.AddSingleton<IRequestIgnorerStatusCodeProvider, DefaultRequestIgnorerStatusCodeProvider>();
            services.AddSingleton<IRequestIgnorerContentTypeProvider, DefaultRequestIgnorerContentTypeProvider>();
            services.AddSingleton<IExtensionProvider<IRequestIgnorer>, DefaultExtensionProvider<IRequestIgnorer>>();
            services.AddSingleton<IExtensionProvider<IInspectorFunction>, DefaultExtensionProvider<IInspectorFunction>>();
            services.AddSingleton<IExtensionProvider<IInspector>, DefaultExtensionProvider<IInspector>>();
            services.AddSingleton<IExtensionProvider<IAgentStartup>, DefaultExtensionProvider<IAgentStartup>>();

            //
            // Messages.
            //
            services.AddSingleton<IMessageConverter, DefaultMessageConverter>();
            services.AddTransient<IMessagePayloadFormatter, DefaultMessagePayloadFormatter>();
            services.AddTransient<IMessageIndexProcessor, DefaultMessageIndexProcessor>();
            services.AddTransient<IMessageTypeProcessor, DefaultMessageTypeProcessor>();

            //
            // Common
            //
            services.AddTransient<IAgentStartupManager, DefaultAgentStartupManager>();
            services.AddTransient<IRequestIgnorerManager, DefaultRequestIgnorerManager>();
            services.AddTransient<IInspectorFunctionManager, DefaultInspectorFunctionManager>();
            services.AddTransient<IExceptionProcessor, ExceptionProcessor>();
            services.AddTransient<WebDiagnosticsInspector>();

            if (!services.Any(s => s.ServiceType == typeof(IScriptOptionsProvider)))
                services.AddSingleton<IScriptOptionsProvider, OptionsScriptOptionsProvider>();

            return services;
        }
    }
}