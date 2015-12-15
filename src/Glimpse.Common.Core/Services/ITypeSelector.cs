using System;
using System.Collections.Generic;
using System.Reflection;

namespace Glimpse.Services
{
    public interface ITypeSelector
    {
        IEnumerable<TypeInfo> FindTypes(IEnumerable<Assembly> targetAssmblies, TypeInfo targetTypeInfo);
    }
}