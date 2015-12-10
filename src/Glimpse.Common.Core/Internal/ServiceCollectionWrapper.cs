using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Glimpse.Internal
{
    public class ServiceCollectionWrapper : IServiceCollection
    {
        private readonly IServiceCollection _innerCollection;

        public ServiceCollectionWrapper(IServiceCollection innerCollection)
        {
            _innerCollection = innerCollection;
        }

        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return _innerCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /*
        public IServiceCollection Add(ServiceDescriptor descriptor)
        {
            _innerCollection.Add(descriptor);
            return this;
        }

        public IServiceCollection Add(IEnumerable<ServiceDescriptor> descriptors)
        {
            _innerCollection.Add(descriptors);
            return this;
        }
        */
        public void Add(ServiceDescriptor item)
        {
            _innerCollection.Add(item);
        }

        public void Clear()
        {
            _innerCollection.Clear();
        }

        public bool Contains(ServiceDescriptor item)
        {
            return _innerCollection.Contains(item);
        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            _innerCollection.CopyTo(array, arrayIndex);
        }

        public bool Remove(ServiceDescriptor item)
        {
            return _innerCollection.Remove(item);
        }

        public int Count
        {
            get { return _innerCollection.Count; }
        }

        public bool IsReadOnly
        {
            get { return _innerCollection.IsReadOnly; }
        }

        public int IndexOf(ServiceDescriptor item)
        {
            return _innerCollection.IndexOf(item);
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            _innerCollection.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _innerCollection.RemoveAt(index);
        }

        public ServiceDescriptor this[int index]
        {
            get { return _innerCollection[index]; }
            set { _innerCollection[index] = value; }
        }
    }
}