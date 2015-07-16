using System;
using System.Threading.Tasks;

namespace Glimpse.Agent
{
    public interface IAgentBroker
    {
        IObservable<MessageListenerOptions> Listen<T>();

        IObservable<MessageListenerOptions> ListenIncludeLatest<T>();

        IObservable<MessageListenerOptions> ListenAll();

        IObservable<MessageListenerOptions> ListenAllIncludeLatest();

        void SendMessage(object message);

        MessageContext Context { get; }
    }
}
