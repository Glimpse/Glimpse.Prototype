using System;
using System.Collections.Generic;
using System.Reflection;

namespace Glimpse.Common
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<Type> BaseTypes(this TypeInfo typeInfo, bool includeSelf = false)
        {
            if (includeSelf)
                yield return typeInfo.AsType();

            var baseType = typeInfo.BaseType;

            while (baseType != null)
            {
                yield return baseType;

                baseType = baseType.GetTypeInfo().BaseType;
            }
        }

        public static string KebabCase(this Type type)
        {
            return type.Name.KebabCase().Replace("i-", ""); // handle IFoo interface naming
        }
    }
}
