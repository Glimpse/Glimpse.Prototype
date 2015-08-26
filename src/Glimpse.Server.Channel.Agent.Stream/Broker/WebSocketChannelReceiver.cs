using Microsoft.AspNet.SignalR;

namespace Glimpse.Server.Web
{
    public class WebSocketChannelReceiver : Hub // Temp dont want this to be public
    {
        private readonly IServerBroker _serverBroker;

        public WebSocketChannelReceiver(IServerBroker serverBroker)
        {
            _serverBroker = serverBroker;
        }

        public void HandleMessage(Message message)
        {
            _serverBroker.SendMessage(message); 
        }
    }
}