using Glimpse.Agent.Inspectors;
using Glimpse.Common.Initialization;
using Microsoft.Extensions.DependencyInjection;

namespace Glimpse
{
    public class AgentSystemWebServices : IRegisterServices
    {
        public void RegisterServices(IServiceCollection services)
        {
            //
            // Common
            //
            services.AddSingleton<IInspectorRuntimeManager, InspectorRuntimeManager>();
        }
    }
}
