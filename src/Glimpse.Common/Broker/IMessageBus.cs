using System;

namespace Glimpse
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