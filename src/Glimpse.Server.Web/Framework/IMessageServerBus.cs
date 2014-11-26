using System;

namespace Glimpse.Server
{
    public interface IMessageServerBus
    {
        IObservable<T> Listen<T>()
            where T : IMessageEnvelope;

        IObservable<T> ListenIncludeLatest<T>()
            where T : IMessageEnvelope;

        void SendMessage(IMessageEnvelope message);
    }
}