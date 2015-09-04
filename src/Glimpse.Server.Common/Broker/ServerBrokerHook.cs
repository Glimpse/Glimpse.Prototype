using System;
using System.Reactive.Linq;
using System.Reflection;

namespace Glimpse.Server
{
    public class ServerBrokerHook
    {
        private readonly IObservable<IMessage> _observable;

        public ServerBrokerHook(IObservable<IMessage> observable)
        {
            _observable = observable;
        }

        // TODO: This doesn't make sense but maybe a Listen that taps
        //       into the string type on IMessage does
        //public IObservable<IMessage> Listen<T>()
        //{
        //    return _observable.Where(opts => typeof(T).GetTypeInfo().IsAssignableFrom(opts.Payload.GetType().GetTypeInfo()));
        //}

        public IObservable<IMessage> ListenAll()
        {
            return _observable;
        }
    }
}