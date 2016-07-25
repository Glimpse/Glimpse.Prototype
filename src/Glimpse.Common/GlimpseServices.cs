using System.Collections.Generic;
using Glimpse.Common;
using Glimpse.Common.Initialization;
using Glimpse.Common.Internal.Serialization;
using Glimpse.Initialization;
using Microsoft.Extensions.DependencyInjection;
using Glimpse.Internal;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Glimpse
{
    public class GlimpseServices
    {
        public static IEnumerable<ServiceDescriptor> GetDefaultServices()
        {
            var services = new ServiceCollection();

            //
            // System
            //
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //
            // Discovery & Reflection.
            //
            services.AddTransient<ITypeActivator, DefaultTypeActivator>();
            services.AddTransient<ITypeSelector, DefaultTypeSelector>();
            services.AddTransient<IAssemblyProvider, DefaultAssemblyProvider>();
            services.AddTransient<ITypeService, DefaultTypeService>();
            // TODO: consider making above singleton 

            services.AddSingleton<IExtensionProvider<IRegisterServices>, DefaultExtensionProvider<IRegisterServices>>();
            services.AddSingleton<IExtensionProvider<IRegisterMiddleware>, DefaultExtensionProvider<IRegisterMiddleware>>();

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