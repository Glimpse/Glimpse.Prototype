using System;

namespace Glimpse.Server
{
    public class LocalMessagePublisher : IMessagePublisher
    {
        public void PublishMessage(IMessage message)
        {
            // TODO: push straight to the bus
        }
    }
}