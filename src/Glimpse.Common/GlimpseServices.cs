using Glimpse;
using Microsoft.Framework.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Reflection;

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
            // Messages.
            //
            services.AddSingleton<IMessageConverter, DefaultMessageConverter>();
            services.AddTransient<IMessagePayloadFormatter, DefaultMessagePayloadFormatter>();
            services.AddTransient<IMessageIndexProcessor, DefaultMessageIndexProcessor>();
            services.AddTransient<IMessageTypeProcessor, DefaultMessageTypeProcessor>();

            //
            // JSON.Net.
            //
            services.AddTransient<JsonSerializer, JsonSerializer>();

            return services;
        }
    }
}