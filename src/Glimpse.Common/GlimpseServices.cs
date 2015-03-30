using Glimpse;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace Glimpse
{
    public class GlimpseServices
    {
        public static IEnumerable<IServiceDescriptor> GetDefaultServices()
        {
            var services = new ServiceCollection();

            //
            // Discovery & Reflection.
            //
            services.AddTransient<ITypeActivator, DefaultTypeActivator>();
            services.AddTransient<ITypeSelector, DefaultTypeSelector>();
            services.AddTransient<IAssemblyProvider, DefaultAssemblyProvider>();
            services.AddTransient<ITypeService, DefaultTypeService>();
            services.AddTransient(typeof(IDiscoverableCollection<>), typeof(ReflectionDiscoverableCollection<>));
            // TODO: consider making above singleton 

            //
            // Context.
            //
            services.AddTransient(typeof(IContextData<>), typeof(ContextData<>)); 

            //
            // Messages.
            //
            services.AddSingleton<IMessageConverter, DefaultMessageConverter>();

            //
            // JSON.Net.
            //
            services.AddTransient<JsonSerializer, JsonSerializer>();

            return services;
        }
    }
}