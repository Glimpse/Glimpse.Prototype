using Glimpse.Agent;
using System;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    public class InProcessChannel : IChannelSender
    {
        private readonly IServerBroker _messageBus;
        private readonly IMessageConverter _messageConverter;

        public InProcessChannel(IServerBroker messageBus, IMessageConverter messageConverter)
        {
            _messageBus = messageBus;
            _messageConverter = messageConverter;
        }

        public async Task PublishMessage(object payload)
        {
            var message = _messageConverter.ConvertMessage(payload);

            await _messageBus.SendMessage(message);
        }
    }
}