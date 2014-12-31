using Glimpse.Server.Resources;
using Microsoft.AspNet.SignalR.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    public class DefaultMessageClientPublisher : IMessageClientPublisher
    {
        private readonly IConnectionManager _connectionManager;

        public DefaultMessageClientPublisher(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public Task PublishMessage(IMessageEnvelope message)
        {
            return Task.Run(() =>
            {
                var hubContext = _connectionManager.GetHubContext<RemoteStreamMessageClientPublisherResource>();
                hubContext.Clients.All.recieveMessage(message);
            });
        }
    }
}