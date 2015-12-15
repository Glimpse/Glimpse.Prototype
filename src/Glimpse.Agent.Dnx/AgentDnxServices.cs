using Glimpse.Agent.Inspectors;
using Glimpse.Agent.Internal.Inspectors;
using Glimpse.Common.Initialization;
using Glimpse.Initialization;
using Glimpse.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Glimpse
{
    public class AgentDnxServices : IRegisterServices
    {
        public void RegisterServices(IServiceCollection services)
        {
            //
            // Common
            //
            services.AddTransient<IInspectorFunctionManager, DefaultInspectorFunctionManager>();
            services.AddTransient<WebDiagnosticsInspector>();
            // TODO: make work outside of DNX
            services.AddTransient<IExceptionProcessor, ExceptionProcessor>();
            
            //
            // Options
            //
            services.AddSingleton<IExtensionProvider<IInspectorFunction>, DefaultExtensionProvider<IInspectorFunction>>();
        }
    }
}
