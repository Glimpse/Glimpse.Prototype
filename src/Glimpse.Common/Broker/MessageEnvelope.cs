using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Glimpse
{

    public class MessageEnvelope : IMessageEnvelope
    {
        private readonly IDictionary<string, object> _indices;

        public MessageEnvelope()
        {
            _indices = new Dictionary<string, object>();
        }

        public string Type { get; set; }

        public string Payload { get; set; }

        public MessageContext Context { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public IReadOnlyDictionary<string, object> Indices
        {
            get { return new ReadOnlyDictionary<string, object>(_indices); }
        }
    }
}