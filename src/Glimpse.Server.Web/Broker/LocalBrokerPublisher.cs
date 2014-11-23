using System;

namespace Glimpse.Server
{
    public class LocalBrokerPublisher : IBrokerPublisher
    {
        public void PublishMessage(IMessage message)
        {
            // TODO: push straight to the bus
        }
    }
}