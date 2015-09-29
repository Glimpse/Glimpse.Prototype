using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Glimpse
{
    public static class TypeExtension
    {
        public static IEnumerable<Type> GetInheritancHierarchy(this Type target)
        {
            for (var next = target; next != null; next = next.GetTypeInfo().BaseType)
            {
                yield return next;
            }
        }
    }
}
