using System;
using System.Collections.Generic;

namespace Glimpse.Agent.Web.Options
{
    public static class IgnoreContentTypeDescriptorExtensions
    {
        public static IgnoreContentTypeDescriptor Add(this IList<IgnoreContentTypeDescriptor> descriptors, string contentType)
        {
            var descriptor = new IgnoreContentTypeDescriptor(contentType);
            descriptors.Add(descriptor);
            return descriptor;
        }

        public static IgnoreContentTypeDescriptor Insert(this IList<IgnoreContentTypeDescriptor> descriptors, int index, string contentType)
        {
            if (index < 0 || index > descriptors.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            var descriptor = new IgnoreContentTypeDescriptor(contentType);
            descriptors.Insert(index, descriptor);
            return descriptor;
        }
    }
}