using System;
using System.Reactive.Linq;
using System.Reflection;

namespace Glimpse.Agent
{
    public class AgentBrokerHook
    {
        private readonly IObservable<AgentBrokerData> _observable;

        public AgentBrokerHook(IObservable<AgentBrokerData> observable)
        {
            _observable = observable;
        }

        public IObservable<AgentBrokerData> Listen<T>()
        {
            return _observable.Where(opts => typeof(T).GetTypeInfo().IsAssignableFrom(IntrospectionExtensions.GetTypeInfo(opts.Payload.GetType())));
        }

        public IObservable<AgentBrokerData> ListenAll()
        {
            return _observable;
        }
    }
}