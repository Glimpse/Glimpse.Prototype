using Glimpse.Agent.Connection.Stream.Connection;
using System;

namespace Glimpse.Agent
{
    public class RemoteStreamMessagePublisher : BaseMessagePublisher
    {
        private readonly IStreamProxy _messagePublisherHub;

        public RemoteStreamMessagePublisher(IStreamProxy messagePublisherHub)
        {
            _messagePublisherHub = messagePublisherHub;
        }

        public override void PublishMessage(IMessage message)
        {
            var newMessage = ConvertMessage(message);

            _messagePublisherHub.UseSender(x => x.Invoke("HandleMessage", newMessage));
        } 
    }
}