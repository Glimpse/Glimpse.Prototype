using System;
using System.Collections.Generic;

namespace Glimpse
{
    public interface ITypeService
    {
        IEnumerable<object> Resolve(string coreLibrary, Type targetType);

        IEnumerable<T> Resolve<T>(string coreLibrary);
    }
}