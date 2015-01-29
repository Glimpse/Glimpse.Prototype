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

        public IMessageEnvelope ConvertMessage(IMessage message)
        { 
            var newMessage = new MessageEnvelope();
            newMessage.Type = message.GetType().FullName;
            newMessage.Payload = _jsonSerializer.Serialize(message);
            newMessage.Context = _contextData.Value;
            
            return newMessage;
        } 
    }
}