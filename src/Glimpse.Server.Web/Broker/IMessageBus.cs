using System;

namespace Glimpse.Server
{
    public interface IMessageBus
    {
        IObservable<T> Listen<T>()
            where T : IMessage;

        IObservable<T> ListenIncludeLatest<T>()
            where T : IMessage;

        void SendMessage(IMessage message);
    }
}