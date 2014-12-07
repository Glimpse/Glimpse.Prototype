using Glimpse.Agent.Connection.Stream.Connection;
using System;
using System.Threading.Tasks;

namespace Glimpse.Agent
{
    public class RemoteStreamMessagePublisher : IMessagePublisher
    {
        private readonly IMessageConverter _messageConverter;
        private readonly IStreamProxy _messagePublisherHub;

        public RemoteStreamMessagePublisher(IStreamProxy messagePublisherHub, IMessageConverter messageConverter)
        {
            _messagePublisherHub = messagePublisherHub;
            _messageConverter = messageConverter;
        }

        public async Task PublishMessage(IMessage message)
        {
            var newMessage = _messageConverter.ConvertMessage(message);

            await _messagePublisherHub.UseSender(x => x.Invoke("HandleMessage", newMessage));
        } 
    }
}