using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    public class WebSocketChannelReceiver : Hub // Temp dont want this to be public
    {
        private readonly IServerBroker _serverBroker;

        public WebSocketChannelReceiver(IServerBroker serverBroker)
        {
            _serverBroker = serverBroker;
        }

        public async Task HandleMessage(Message message)
        {
            await _serverBroker.SendMessage(message); 
        }
    }
}