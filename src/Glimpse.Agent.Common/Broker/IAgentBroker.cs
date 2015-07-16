using System;
using System.Threading.Tasks;

namespace Glimpse.Agent
{
    public interface IAgentBroker
    {
        IObservable<MessageListenerOptions> Listen<T>();

        IObservable<MessageListenerOptions> ListenIncludeLatest<T>();

        void SendMessage(object message);
    }
}