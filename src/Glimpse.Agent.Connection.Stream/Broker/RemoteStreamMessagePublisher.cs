using System;

namespace Glimpse.Agent
{
    public class RemoteStreamMessagePublisher : BaseMessagePublisher
    {
        public override void PublishMessage(IMessage message)
        {
            var newMessage = ConvertMessage(message);

            // TODO: Use SignalR to publish message
        }
    }
}