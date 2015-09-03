using System;

namespace Glimpse.Server.Web
{
    // TODO: Review how we think people will want to filter on these messages given 
    //       the lack of structure

    public interface IServerBroker
    {
        //IObservable<MessageListenerPayload> ListenAll();

        //IObservable<MessageListenerPayload> ListenAllIncludeLatest();

        void SendMessage(IMessage message);
    }
}