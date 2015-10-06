using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Glimpse.Extensions;

namespace Glimpse.Internal
{
    public class DefaultMessageTypeProcessor : IMessageTypeProcessor
    {
        private readonly static Type[] _exclusions = { typeof(object) };

        public virtual IEnumerable<string> Derive(object payload)
        {
            var typeInfo = payload.GetType().GetTypeInfo();

            return typeInfo.BaseTypes(true)
                .Concat(typeInfo.ImplementedInterfaces)
                .Except(_exclusions)
                .Select(t =>
                {
                    var result = t.KebabCase();

                    if (result.EndsWith("-message"))
                        return result.Substring(0, result.Length - 8);

                    return result;
                });
        }
    }
}