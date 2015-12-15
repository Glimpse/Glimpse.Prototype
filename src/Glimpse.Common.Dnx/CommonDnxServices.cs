using System.Collections.Generic;
using Glimpse.Internal;
using Glimpse.Common.Initialization;
using Glimpse.Initialization;
using Glimpse.Platform;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Glimpse
{
    public static class CommonDnxServices
    {
        public static IEnumerable<ServiceDescriptor> GetDefaultServices()
        {
            var services = new ServiceCollection();

            //
            // Discovery & Reflection.
            //
            services.AddSingleton(PlatformServices.Default.AssemblyLoadContextAccessor);
            services.AddSingleton(PlatformServices.Default.AssemblyLoaderContainer);
            services.AddSingleton(PlatformServices.Default.LibraryManager);
            services.AddTransient<IAssemblyProvider, LibraryManagerAssemblyProvider>();

            //
            // Extensions
            //
            services.AddSingleton<IExtensionProvider<IRegisterMiddleware>, DefaultExtensionProvider<IRegisterMiddleware>>();

            return services;
        }
    }
}
