using Glimpse.Agent;
using System;

namespace Glimpse.Server
{
    public class LocalMessagePublisher : BaseMessagePublisher
    {
        private readonly IMessageServerBus _messageBus;

        public LocalMessagePublisher(IMessageServerBus messageBus)
        {
            _messageBus = messageBus;
        }

        public override void PublishMessage(IMessage message)
        {
            var newMessage = ConvertMessage(message);

            _messageBus.SendMessage(newMessage);
        }
    }
}