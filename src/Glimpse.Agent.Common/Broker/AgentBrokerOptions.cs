using System;
using System.Reactive.Linq;
using System.Reflection;

namespace Glimpse.Agent
{
    public class AgentBrokerOptions
    {
        private readonly IObservable<MessagePayloadData> _observable;

        public AgentBrokerOptions(IObservable<MessagePayloadData> observable)
        {
            _observable = observable;
        }

        public IObservable<MessagePayloadData> Listen<T>()
        {
            return _observable.Where(opts => typeof(T).GetTypeInfo().IsAssignableFrom(IntrospectionExtensions.GetTypeInfo(opts.Payload.GetType())));
        }

        public IObservable<MessagePayloadData> ListenAll()
        {
            return _observable;
        }
    }
}