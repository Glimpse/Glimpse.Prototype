using System.Collections.Generic;
using Glimpse.Common;
using Glimpse.Common.Internal.Serialization;
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
            services.AddTransient<IGlimpseContextAccessor, DefaultGlimpseContextAccessor>(); 

            //
            // JSON.Net.
            //
            services.AddTransient<JsonSerializer, JsonSerializer>();
            services.AddSingleton<IJsonSerializerProvider, DefaultJsonSerializerProvider>();

            return services;
        }
    }
}