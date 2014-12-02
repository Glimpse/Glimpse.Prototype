using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;

namespace Glimpse.Server.Resources
{
    public class RemoteStreamMessagePublisherResource : Hub // Temp dont want this to be public
    {
        private readonly IMessageServerBus _messageServerBus;

        public RemoteStreamMessagePublisherResource(IMessageServerBus messageServerBus)
        {
            _messageServerBus = messageServerBus;
        }

        public async Task HandleMessage(MessageEnvelope envelope)
        {
            await _messageServerBus.SendMessage(envelope); 
        }
    }
}