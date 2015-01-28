using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;

namespace Glimpse.Server.Resources
{
    public class WebSocketChannelReceiver : Hub // Temp dont want this to be public
    {
        private readonly IServerBroker _messageServerBus;

        public WebSocketChannelReceiver(IServerBroker messageServerBus)
        {
            _messageServerBus = messageServerBus;
        }

        public async Task HandleMessage(MessageEnvelope envelope)
        {
            await _messageServerBus.SendMessage(envelope); 
        }
    }
}