using System;

namespace Glimpse.Server
{
    public class LocalMessagePublisher : IMessagePublisher
    {
        private readonly IMessageServerBus _messageBus;

        public LocalMessagePublisher(IMessageServerBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void PublishMessage(IMessage message)
        {
            _messageBus.SendMessage(message);
        }
    }
}