using System;

namespace Glimpse.Server
{
    public class LocalMessagePublisher : IMessagePublisher
    {
        private readonly IMessageBus _messageBus;

        public LocalMessagePublisher(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void PublishMessage(IMessage message)
        {
            _messageBus.SendMessage(message);
        }
    }
}