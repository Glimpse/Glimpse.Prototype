using Glimpse.Agent.Connection.Stream.Connection;
using System;
using System.Threading.Tasks;

namespace Glimpse.Agent
{
    public class RemoteStreamMessagePublisher : BaseMessagePublisher
    {
        private readonly IStreamProxy _messagePublisherHub;

        public RemoteStreamMessagePublisher(IStreamProxy messagePublisherHub)
        {
            _messagePublisherHub = messagePublisherHub;
        }

        public override async Task PublishMessage(IMessage message)
        {
            var newMessage = ConvertMessage(message);

            await _messagePublisherHub.UseSender(x => x.Invoke("HandleMessage", newMessage));
        } 
    }
}