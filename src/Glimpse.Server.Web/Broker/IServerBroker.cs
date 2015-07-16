using System;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    // TODO: Review how we think people will want to filter on these messages given 
    //       the lack of structure

    public interface IServerBroker
    {
        IObservable<MessageListenerOptions> ListenAll();

        IObservable<MessageListenerOptions> ListenAllIncludeLatest();

        void SendMessage(IMessage message);
    }
}