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

        public IMessage ConvertMessage(object payload)
        { 
            var message = new Message();
            message.Type = payload.GetType().FullName;
            message.Payload = _jsonSerializer.Serialize(payload);
            message.Context = _contextData.Value;

            ProcessTags(payload, message);

            return message;
        } 

        private void ProcessTags(object payload, Message newMessage)
        {
            var tagMessage = payload as IMessageTag;
            if (tagMessage != null)
            {
                newMessage.Tags = tagMessage.Tags;
            }
        }
    }
}