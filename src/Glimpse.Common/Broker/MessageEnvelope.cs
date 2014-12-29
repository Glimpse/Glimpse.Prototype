using System;

namespace Glimpse
{

    public class MessageEnvelope : IMessageEnvelope
    {
        public string Type { get; set; }

        public string Payload { get; set; }

        public IMessageContext Context { get; set; }
    }
}