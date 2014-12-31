using Glimpse.Agent;
using System;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    public class LocalMessagePublisher : IMessagePublisher
    {
        private readonly IMessageServerBus _messageBus;
        private readonly IMessageConverter _messageConverter;

        public LocalMessagePublisher(IMessageServerBus messageBus, IMessageConverter messageConverter)
        {
            _messageBus = messageBus;
            _messageConverter = messageConverter;
        }

        public async Task PublishMessage(IMessage message)
        {
            var newMessage = _messageConverter.ConvertMessage(message);

            await _messageBus.SendMessage(newMessage);
        }
    }
}