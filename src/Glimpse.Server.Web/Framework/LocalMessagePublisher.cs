using Glimpse.Agent;
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
            // TODO: Probably want to convert the message to JSON at this point

            var newMessage = new MessageEnvelope();
            newMessage.Type = message.GetType().FullName;
            newMessage.Message = message;

            _messageBus.SendMessage(newMessage);
        }
    }
}