using System;

namespace Glimpse.Broker
{
    public interface IBrokerPublisher
    {
        void PublishMessage(IMessage message);
    }
}