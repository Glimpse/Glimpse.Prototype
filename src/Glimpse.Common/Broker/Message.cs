using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Glimpse
{

    public class Message : IMessage
    {
        public Guid Id { get; set; }

        public IEnumerable<string> Types { get; set; }

        public string Payload { get; set; }

        public int Ordinal { get; set; }

        public MessageContext Context { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public IReadOnlyDictionary<string, object> Indices { get; set; }
    }
}