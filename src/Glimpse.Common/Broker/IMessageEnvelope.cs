using System;
using System.Collections.Generic;

namespace Glimpse
{
    public interface IMessageEnvelope
    {
        string Type { get; }

        string Payload { get; }

        MessageContext Context { get; }
        
        IEnumerable<string> Tags { get; }
    }
}