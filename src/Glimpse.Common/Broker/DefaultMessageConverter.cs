using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

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
            message.Id = Guid.NewGuid();
            message.Type = payload.GetType().FullName;
            message.Payload = _jsonSerializer.Serialize(payload);
            message.Context = _contextData.Value;

            ProcessIndices(payload, message);
            ProcessTags(payload, message);

            return message;
        } 

        private void ProcessIndices(object payload, Message message)
        {
            var indicesMessage = payload as IMessageIndices;
            if (indicesMessage?.Indices != null)
            {
                message.Indices = indicesMessage.Indices;
                indicesMessage.Indices = null;
            } 
        }

        private void ProcessTags(object payload, Message message)
        {
            var tagMessage = payload as IMessageTag;
            if (tagMessage?.Tags != null)
            {
                message.Tags = tagMessage.Tags;
                tagMessage.Tags = null;
            }
        }
    }
}