using Glimpse.Server.Resources;
using Microsoft.AspNet.SignalR.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    public class DefaultClientBroker : IClientBroker
    {
        private readonly IConnectionManager _connectionManager;

        public DefaultClientBroker(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public Task PublishMessage(IMessageEnvelope message)
        {
            return Task.Run(() =>
            {
                var hubContext = _connectionManager.GetHubContext<WebSocketClientChannelSender>();
                hubContext.Clients.All.recieveMessage(message);
            });
        }
    }
}