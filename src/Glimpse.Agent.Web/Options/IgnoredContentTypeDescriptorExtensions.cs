using System;
using System.Collections.Generic;

namespace Glimpse.Agent.Web.Options
{
    public static class IgnoredContentTypeDescriptorExtensions
    {
        public static IgnoredContentTypeDescriptor Add(this IList<IgnoredContentTypeDescriptor> descriptors, string contentType)
        {
            var descriptor = new IgnoredContentTypeDescriptor(contentType);
            descriptors.Add(descriptor);
            return descriptor;
        }

        public static IgnoredContentTypeDescriptor Insert(this IList<IgnoredContentTypeDescriptor> descriptors, int index, string contentType)
        {
            if (index < 0 || index > descriptors.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            var descriptor = new IgnoredContentTypeDescriptor(contentType);
            descriptors.Insert(index, descriptor);
            return descriptor;
        }
    }
}