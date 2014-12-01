using Microsoft.AspNet.SignalR;
using System;

namespace Glimpse.Server.Resources
{
    public class RemoteStreamMessagePublisherResource : Hub // Temp dont want this to be public
    {
        private readonly IMessageServerBus _messageServerBus;

        public RemoteStreamMessagePublisherResource(IMessageServerBus messageServerBus)
        {
            _messageServerBus = messageServerBus;
        }

        public void HandleMessage(MessageEnvelope envelope)
        {
            _messageServerBus.SendMessage(envelope); 
        }
    }
}