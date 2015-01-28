using System;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    // TODO: Review how we think people will want to filter on these messages given 
    //       the lack of structure

    public interface IServerBroker
    {
        IObservable<T> Listen<T>()
            where T : IMessageEnvelope;

        IObservable<T> ListenIncludeLatest<T>()
            where T : IMessageEnvelope;

        Task SendMessage(IMessageEnvelope message);
    }
}