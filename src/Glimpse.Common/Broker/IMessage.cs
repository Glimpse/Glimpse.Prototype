using System;
using System.Collections.Generic;

namespace Glimpse
{
    public interface IMessage
    {
        string Type { get; }

        string Payload { get; }

        MessageContext Context { get; }
        
        IEnumerable<string> Tags { get; } // TODO: Move into a key inside of Indices

        IReadOnlyDictionary<string, object> Indices { get; }
    }
}