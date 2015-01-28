using System;
using System.Threading.Tasks;

namespace Glimpse.Agent
{
    public interface IAgentBroker
    {
        IObservable<T> Listen<T>()
            where T : IMessage;

        IObservable<T> ListenIncludeLatest<T>()
            where T : IMessage;

        Task SendMessage(IMessage message);
    }
}