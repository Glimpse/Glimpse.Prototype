using System.Collections.Generic;
using Glimpse.Common;
using Glimpse.Common.Internal.Serialization;
using Glimpse.Initialization;
using Glimpse.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Glimpse.Internal
{
    public class CommonServices
    {
        public static IEnumerable<ServiceDescriptor> GetDefaultServices()
        {
            var services = new ServiceCollection();

            //
            // Discovery & Reflection.
            //
            services.AddTransient<ITypeActivator, DefaultTypeActivator>();
            services.AddTransient<ITypeSelector, DefaultTypeSelector>();
            services.AddTransient<ITypeService, DefaultTypeService>();

            //
            // Extensions
            //
            services.AddSingleton<IExtensionProvider<IRegisterServices>, DefaultExtensionProvider<IRegisterServices>>();

            //
            // Context.
            //
            services.AddSingleton(typeof(IContextData<>), typeof(ContextData<>)); 
            services.AddSingleton<IGlimpseContextAccessor, DefaultGlimpseContextAccessor>(); 

            //
            // JSON.Net.
            //
            services.AddTransient<JsonSerializer, JsonSerializer>();
            services.AddSingleton<IJsonSerializerProvider, DefaultJsonSerializerProvider>();

            return services;
        }
    }
}