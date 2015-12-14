using Glimpse.Internal;
using Glimpse.Common.Initialization;
using Microsoft.Extensions.DependencyInjection;

namespace Glimpse
{
    public class CommonSystemWebServices : IRegisterServices
    {
        public void RegisterServices(IServiceCollection services)
        {
            //
            // Discovery & Reflection.
            //
            services.AddTransient<IAssemblyProvider, AppDomainAssemblyProvider>();
        }
    }
}
