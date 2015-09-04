using System;

namespace Glimpse.Agent
{
    public class MessagePayloadData
    {
        public MessagePayloadData(object payload, MessageContext context)
        {
            Payload = payload;
            Context = context;
        }

        public object Payload { get; set; }

        public MessageContext Context { get; set; }
    }
}