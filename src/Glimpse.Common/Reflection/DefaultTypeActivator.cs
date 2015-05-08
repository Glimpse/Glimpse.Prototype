using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse
{
    public class DefaultTypeActivator : ITypeActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultTypeActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object CreateInstance(Type instanceType, params object[] parameters)
        { 
            var activated = ActivatorUtilities.CreateInstance(_serviceProvider, instanceType, parameters);

            return activated;
        }

        public T CreateInstance<T>(params object[] parameters)
        {
            var activated = ActivatorUtilities.CreateInstance(_serviceProvider, typeof(T), parameters);

            return (T)activated;
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
            return ActivatorUtilities.CreateInstance(_serviceProvider, type.AsType());
        }
    }
}