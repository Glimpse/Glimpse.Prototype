using System;
using System.Collections.Generic;

namespace Glimpse.Reflection
{
    public interface ITypeService
    {
        IEnumerable<object> Resolve(string coreLibrary, Type targetType);

        IEnumerable<T> Resolve<T>(string coreLibrary);
    }
}