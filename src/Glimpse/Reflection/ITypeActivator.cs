using System;
using System.Collections.Generic;
using System.Reflection;

namespace Glimpse.Reflection
{
    public interface ITypeActivator
    {
        IEnumerable<object> CreateInstances(IEnumerable<TypeInfo> types);

        IEnumerable<T> CreateInstances<T>(IEnumerable<TypeInfo> types);
    }
}