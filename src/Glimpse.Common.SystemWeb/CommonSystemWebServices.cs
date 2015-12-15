using System.Collections.Generic;
using Glimpse.Internal;
using Glimpse.Initialization;
using Glimpse.Platform;
using Microsoft.Extensions.DependencyInjection;

namespace Glimpse
{
    public static class CommonSystemWebServices
    {
        public static IEnumerable<ServiceDescriptor> GetDefaultServices()
        {
            var services = new ServiceCollection();

            //
            // Discovery & Reflection.
            //
            services.AddTransient<IAssemblyProvider, AppDomainAssemblyProvider>();

            return services;
        }
    }
}
