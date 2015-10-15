using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Glimpse.Common.Messaging;
using Glimpse.Internal.Extensions;

namespace Glimpse.Agent.Internal.Messaging
{
    public class DefaultMessageTypeProcessor : IMessageTypeProcessor
    {
        private readonly static Type[] _exclusions = { typeof(object), typeof(IMessageTypeProvider) };

        public virtual IEnumerable<string> Derive(object payload)
        {
            var typeInfo = payload.GetType().GetTypeInfo();

            var result = typeInfo.BaseTypes(true)
                .Concat(typeInfo.ImplementedInterfaces)
                .Except(_exclusions)
                .Select(t => t.KebabCase());

            var providedTypes = (payload as IMessageTypeProvider)?.Types;
            if (providedTypes != null)
            {
                result = result.Concat(providedTypes.Select(t => t.KebabCase()));
            }

            return result
                .Select(type => type.EndsWith("-message") ? type.Substring(0, type.Length - 8) : type) // remove -message suffix
                .Distinct();
        }
    }
}