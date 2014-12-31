using Glimpse;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace Glimpse
{
    public class GlimpseServices
    {
        public static IEnumerable<IServiceDescriptor> GetDefaultServices()
        {
            return GetDefaultServices(new Configuration());
        }

        public static IEnumerable<IServiceDescriptor> GetDefaultServices(IConfiguration configuration)
        {
            var describe = new ServiceDescriber(configuration);
            
            //
            // Discovery & Reflection.
            //
            yield return describe.Transient<ITypeActivator, DefaultTypeActivator>();
            yield return describe.Transient<ITypeSelector, DefaultTypeSelector>();
            yield return describe.Transient<IAssemblyProvider, DefaultAssemblyProvider>();
            yield return describe.Transient<ITypeService, DefaultTypeService>();
            yield return describe.Transient(typeof(IDiscoverableCollection<>), typeof(ReflectionDiscoverableCollection<>));
            // TODO: consider making above singleton 

            //
            // Context.
            //
            yield return describe.Transient(typeof(IContextData<>), typeof(ContextData<>)); 

            //
            // Messages.
            //
            yield return describe.Singleton<IMessageConverter, DefaultMessageConverter>();

            //
            // JSON.Net.
            //
            yield return describe.Singleton<JsonSerializer>(x => new JsonSerializer
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }
    }
}