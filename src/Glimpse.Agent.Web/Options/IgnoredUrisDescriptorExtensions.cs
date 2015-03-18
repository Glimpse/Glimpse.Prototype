using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Glimpse.Agent.Web.Options
{
    public static class IgnoredUrisDescriptorExtensions
    {
        public static IgnoredUrisDescriptor Add(this IList<IgnoredUrisDescriptor> descriptors, string format)
        {
            var descriptor = new IgnoredUrisDescriptor(format);
            descriptors.Add(descriptor);
            return descriptor;
        }

        public static IgnoredUrisDescriptor Insert(this IList<IgnoredUrisDescriptor> descriptors, int index, string format)
        {
            if (index < 0 || index > descriptors.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            var descriptor = new IgnoredUrisDescriptor(format);
            descriptors.Insert(index, descriptor);
            return descriptor;
        }

        public static IgnoredUrisDescriptor Add(this IList<IgnoredUrisDescriptor> descriptors, Regex expression)
        {
            var descriptor = new IgnoredUrisDescriptor(expression);
            descriptors.Add(descriptor);
            return descriptor;
        }

        public static IgnoredUrisDescriptor Insert(this IList<IgnoredUrisDescriptor> descriptors, int index, Regex expression)
        {
            if (index < 0 || index > descriptors.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            var descriptor = new IgnoredUrisDescriptor(expression);
            descriptors.Insert(index, descriptor);
            return descriptor;
        }
    }
}