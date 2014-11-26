using System;

namespace Glimpse.Agent
{
    public interface IMessagePublisher
    {
        void PublishMessage(IMessage message);
    }
}