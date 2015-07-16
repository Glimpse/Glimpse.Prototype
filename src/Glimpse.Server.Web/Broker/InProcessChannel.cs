using Glimpse.Agent;
using System;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    public class InProcessChannel : IChannelSender
    {
        private readonly IServerBroker _messageBus;

        public InProcessChannel(IServerBroker messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task PublishMessage(IMessage message)
        {
            await _messageBus.SendMessage(message);
        }
    }
}