using System;

namespace Glimpse.Agent
{
    public interface IMessageAgentBus
    {
        IObservable<T> Listen<T>()
            where T : IMessage;

        IObservable<T> ListenIncludeLatest<T>()
            where T : IMessage;

        void SendMessage(IMessage message);
    }
}