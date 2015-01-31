using System;
using System.Collections.Generic;

namespace Glimpse
{

    public class MessageEnvelope : IMessageEnvelope
    {
        public string Type { get; set; }

        public string Payload { get; set; }

        public MessageContext Context { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}