using Glimpse.Internal;
using Glimpse.Common.Initialization;
using Glimpse.Initialization;
using Microsoft.Extensions.DependencyInjection;

namespace Glimpse
{
    public class CommonDnxServices : IRegisterServices
    {
        public void RegisterServices(IServiceCollection services)
        {
            //
            // Discovery & Reflection.
            //
            services.AddTransient<IAssemblyProvider, LibraryManagerAssemblyProvider>();

            //
            // Extensions
            //
            services.AddSingleton<IExtensionProvider<IRegisterMiddleware>, DefaultExtensionProvider<IRegisterMiddleware>>();
        }
    }
}
