using System;

namespace Glimpse
{
    public interface IBrokerPublisher
    {
        void PublishMessage(IMessage message);
    }
}