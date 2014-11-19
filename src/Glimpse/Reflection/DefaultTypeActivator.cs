using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Glimpse.Reflection
{
    public class DefaultTypeActivator : ITypeActivator
    {
        private readonly Microsoft.Framework.DependencyInjection.ITypeActivator _typeActivator;
        private readonly IServiceProvider _serviceProvider;

        public DefaultTypeActivator(IServiceProvider serviceProvider, Microsoft.Framework.DependencyInjection.ITypeActivator typeActivator)
        {
            _typeActivator = typeActivator;
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<object> CreateInstances(IEnumerable<TypeInfo> types)
        {
            var activated = types.Select(t => CreateInstance(t));

            return activated;
        }

        public IEnumerable<T> CreateInstances<T>(IEnumerable<TypeInfo> types)
        {
            var activated = types.Select(t => (T)CreateInstance(t));

            return activated;
        }

        private object CreateInstance(TypeInfo type)
        {
            return _typeActivator.CreateInstance(_serviceProvider, type.AsType());
        }
    }
}