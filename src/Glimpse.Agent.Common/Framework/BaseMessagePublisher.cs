using System;

namespace Glimpse.Agent
{
    public abstract class BaseMessagePublisher : IMessagePublisher
    {
        public abstract void PublishMessage(IMessage message);

        protected IMessageEnvelope ConvertMessage(IMessage message)
        {
            // TODO: Probably want to convert the message to JSON at this point
            var newMessage = new MessageEnvelope();
            newMessage.Type = message.GetType().FullName;
            newMessage.Message = message;

            return newMessage;
        }
    }
}