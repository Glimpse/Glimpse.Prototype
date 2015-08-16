using Microsoft.AspNet.SignalR.Infrastructure;

namespace Glimpse.Server.Web
{
    public class DefaultClientBroker : IClientBroker
    {
        private readonly IConnectionManager _connectionManager;

        public DefaultClientBroker(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void PublishMessage(IMessage message)
        { 
            var hubContext = _connectionManager.GetHubContext<WebSocketClientChannelSender>();
            hubContext.Clients.All.recieveMessage(message);
        }
    }
}