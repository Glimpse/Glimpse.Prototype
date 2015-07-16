using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Glimpse
{
    public class DefaultMessageConverter : IMessageConverter
    {
        private readonly JsonSerializer _jsonSerializer;
        private readonly IContextData<MessageContext> _contextData;

        public DefaultMessageConverter(JsonSerializer jsonSerializer, IContextData<MessageContext> contextData)
        {
            jsonSerializer.ContractResolver = new CamelCasePropertyNamesContractResolver();

            _jsonSerializer = jsonSerializer;
            _contextData = contextData;
        }

        public IMessageEnvelope ConvertMessage(object payload)
        { 
            var newMessage = new MessageEnvelope();
            newMessage.Type = payload.GetType().FullName;
            newMessage.Payload = _jsonSerializer.Serialize(payload);
            newMessage.Context = _contextData.Value;

            ProcessTags(payload, newMessage);

            return newMessage;
        } 

        private void ProcessTags(object payload, MessageEnvelope newMessage)
        {
            var tagMessage = payload as IMessageTag;
            if (tagMessage != null)
            {
                newMessage.Tags = tagMessage.Tags;
            }
        }
    }
}