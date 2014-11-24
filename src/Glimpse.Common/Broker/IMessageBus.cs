using System;

namespace Glimpse
{
    public interface IMessageBus
    {
        IObservable<T> Listen<T>();

        IObservable<T> ListenIncludeLatest<T>();

        void SendMessage(object message);
    }
}