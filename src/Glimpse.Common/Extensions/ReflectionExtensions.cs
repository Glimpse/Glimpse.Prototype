using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Glimpse.Common
{
    public static class ReflectionExtensions
    {
        private readonly static Regex _regex = new Regex("([A-Z0-9])", RegexOptions.Compiled);

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
            return _regex.Replace(type.Name, Replacement)
                .Replace("i-", ""); // handle IFoo interface naming
        }

        private static string Replacement(Match m)
        {
            var result = m.Value.ToLower();
            return m.Index == 0 ? result : "-" + result;
        }
    }
}
