using System;
using System.Collections.Generic;
using System.Threading;

namespace Glimpse.Agent.Internal.Messaging
{
    // Glimpse servers & clients depend on this behavior. 
    // As such, this type is marked internal to prevent tampering
    internal class DefaultMessageConverter : IMessageConverter
    {
        private static int _ordinal = 0;
        
        public DefaultMessageConverter(IMessagePayloadFormatter payloadFormatter, IMessageIndexProcessor indexProcessor, IMessageTypeProcessor typeProcessor)
        {
            PayloadFormatter = payloadFormatter;
            IndexProcessor = indexProcessor;
            TypeProcessor = typeProcessor;
        }

        private IMessagePayloadFormatter PayloadFormatter { get; }

        private IMessageIndexProcessor IndexProcessor { get; }

        private IMessageTypeProcessor TypeProcessor { get; }

        public IMessage ConvertMessage(object payload, MessageContext context)
        {
            var message = new Message
            {
                Id = Guid.NewGuid(),
                Ordinal = Interlocked.Increment(ref _ordinal),
                Types = GetTypes(payload),
                Context = context,
                Indices = GetIndices(payload)
            };

            message.Payload = GetPayload(message, payload);
            
            return message;
        }

        private string GetPayload(Message message, object payload)
        {
            return PayloadFormatter.Serialize(message, payload);
        }

        private IReadOnlyDictionary<string, object> GetIndices(object payload)
        {
            return IndexProcessor.Derive(payload);
        }

        private IEnumerable<string> GetTypes(object payload)
        {
            return TypeProcessor.Derive(payload);
        }
    }
}