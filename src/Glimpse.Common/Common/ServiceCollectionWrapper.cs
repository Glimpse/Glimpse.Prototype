using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse
{
    public class ServiceCollectionWrapper : IServiceCollection
    {
        private readonly IServiceCollection _innerCollection;

        public ServiceCollectionWrapper(IServiceCollection innerCollection)
        {
            _innerCollection = innerCollection;
        }

        public IEnumerator<IServiceDescriptor> GetEnumerator()
        {
            return _innerCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IServiceCollection Add(IServiceDescriptor descriptor)
        {
            _innerCollection.Add(descriptor);
            return this;
        }

        public IServiceCollection Add(IEnumerable<IServiceDescriptor> descriptors)
        {
            _innerCollection.Add(descriptors);
            return this;
        }
    }
}