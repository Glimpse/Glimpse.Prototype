using System;

namespace Glimpse
{
    public class MessageListenerPayload
    {
        public MessageListenerPayload(object payload, MessageContext context)
        {
            Payload = payload;
            Context = context;
        }

        public object Payload { get; set; }

        public MessageContext Context { get; set; }
    }
}