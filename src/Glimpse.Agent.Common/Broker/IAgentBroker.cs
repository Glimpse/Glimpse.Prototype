using System;
using System.Threading.Tasks;

namespace Glimpse.Agent
{
    public interface IAgentBroker
    {
        IObservable<T> Listen<T>();

        IObservable<T> ListenIncludeLatest<T>();

        void SendMessage(object message);
    }
}