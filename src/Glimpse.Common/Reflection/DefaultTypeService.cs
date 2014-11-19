using System;
using System.Collections.Generic;
using System.Reflection;

namespace Glimpse
{
    public class DefaultTypeService : ITypeService
    {
        private readonly ITypeActivator _typesActivator;
        private readonly ITypeSelector _typeDiscovery;
        private readonly IAssemblyProvider _assemblyProvider;

        public DefaultTypeService(ITypeActivator typesActivator, ITypeSelector typeDiscovery, IAssemblyProvider assemblyProvider)
        {
            _typesActivator = typesActivator;
            _typeDiscovery = typeDiscovery;
            _assemblyProvider = assemblyProvider;
        }

        public IEnumerable<object> Resolve(string coreLibrary, Type targetType)
        {
            var types = DiscoverTypes(coreLibrary, targetType);
            var instances = _typesActivator.CreateInstances(types);

            return instances;
        }

        public IEnumerable<T> Resolve<T>(string coreLibrary)
        {
            var types = DiscoverTypes(coreLibrary, typeof(T));
            var instances = _typesActivator.CreateInstances<T>(types);

            return instances;
        }

        private IEnumerable<TypeInfo> DiscoverTypes(string coreLibrary, Type targetType)
        {
            var assemblies = _assemblyProvider.GetCandidateAssemblies(coreLibrary);
            var types = _typeDiscovery.FindTypes(assemblies, targetType.GetTypeInfo());

            return types;
        }
    }
}