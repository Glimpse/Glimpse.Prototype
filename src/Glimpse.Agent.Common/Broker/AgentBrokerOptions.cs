using System;
using System.Reactive.Linq;
using System.Reflection;

namespace Glimpse.Agent
{
    public class AgentBrokerOptions
    {
        private readonly IObservable<MessageListenerPayload> _observable;

        public AgentBrokerOptions(IObservable<MessageListenerPayload> observable)
        {
            _observable = observable;
        }

        public IObservable<MessageListenerPayload> Listen<T>()
        {
            return _observable.Where(opts => typeof(T).GetTypeInfo().IsAssignableFrom(IntrospectionExtensions.GetTypeInfo(opts.Payload.GetType())));
        }

        public IObservable<MessageListenerPayload> ListenAll()
        {
            return _observable;
        }
    }
}