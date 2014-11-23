using System;

namespace Glimpse.Broker
{
    public interface IMessageBus
    {
        IObservable<T> Listen<T>();

        IObservable<T> ListenIncludeLatest<T>();

        bool IsRegistered(Type type);
    }
}