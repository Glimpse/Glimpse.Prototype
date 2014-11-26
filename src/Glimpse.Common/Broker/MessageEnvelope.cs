using System;

namespace Glimpse
{

    public class MessageEnvelope : IMessageEnvelope
    {
        public string Type { get; set; }

        public object Message { get; set; }
    }
}