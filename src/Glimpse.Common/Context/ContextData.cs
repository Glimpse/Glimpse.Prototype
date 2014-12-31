using System;
#if ASPNET50
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting;
#else
using System.Threading;
#endif

namespace Glimpse
{
    public class ContextData<T> : IContextData<T>
    { 
#if ASPNET50
        private static string Key = typeof(ContextData<T>).FullName;

        public T Value
        {
            get
            { 
                var handle = CallContext.LogicalGetData(Key) as ObjectHandle;
                return handle != null ? (T)handle.Unwrap() : default(T);
            }
            set
            {
                CallContext.LogicalSetData(Key, new ObjectHandle(value));
            }
        }
#else
        private readonly AsyncLocal<T> _serviceProvider = new AsyncLocal<T>();

        public IServiceProvider ServiceProvider
        {
            get { return _serviceProvider.Value; }
            set { _serviceProvider.Value = value; }
        }
#endif
    }
}