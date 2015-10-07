using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Glimpse.Internal;
using Newtonsoft.Json;

namespace Glimpse
{
    public class GlimpseServices
    {
        public static IEnumerable<ServiceDescriptor> GetDefaultServices()
        {
            var services = new ServiceCollection();

            //
            // Discovery & Reflection.
            //
            services.AddTransient<ITypeActivator, DefaultTypeActivator>();
            services.AddTransient<ITypeSelector, DefaultTypeSelector>();
            services.AddTransient<IAssemblyProvider, DefaultAssemblyProvider>();
            services.AddTransient<ITypeService, DefaultTypeService>();
            // TODO: consider making above singleton 

            //
            // Context.
            //
            services.AddTransient(typeof(IContextData<>), typeof(ContextData<>)); 

            //
            // JSON.Net.
            //
            services.AddTransient<JsonSerializer, JsonSerializer>();

            return services;
        }
    }
}