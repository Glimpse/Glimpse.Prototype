using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Glimpse.Agent.Web.Options
{
    public static class IgnoredStatusCodeDescriptorExtensions
    {
        public static IgnoredStatusCodeDescriptor Add(this IList<IgnoredStatusCodeDescriptor> descriptors, int statusCode)
        {
            var descriptor = new IgnoredStatusCodeDescriptor(statusCode);
            descriptors.Add(descriptor);
            return descriptor;
        }

        public static IgnoredStatusCodeDescriptor Insert(this IList<IgnoredStatusCodeDescriptor> descriptors, int index, int statusCode)
        {
            if (index < 0 || index > descriptors.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            var descriptor = new IgnoredStatusCodeDescriptor(statusCode);
            descriptors.Insert(index, descriptor);
            return descriptor;
        }
    }
}