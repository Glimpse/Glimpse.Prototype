using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Glimpse
{
    public class DefaultMessageConverter : IMessageConverter
    {
        private readonly JsonSerializer _jsonSerializer;

        public DefaultMessageConverter(JsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public IMessageEnvelope ConvertMessage(IMessage message)
        {
            var newMessage = new MessageEnvelope();
            newMessage.Type = message.GetType().FullName;
            newMessage.Payload = Serialize(message);

            return newMessage;
        }

        protected string Serialize(object data)
        {
            // Brought across from - https://github.com/JamesNK/Newtonsoft.Json/blob/master/Src/Newtonsoft.Json/JsonConvert.cs#L635
            var stringBuilder = new StringBuilder(256);
            using (var stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
            using (var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = _jsonSerializer.Formatting })
            { 
                _jsonSerializer.Serialize(jsonWriter, data, data.GetType());

                return stringWriter.ToString();
            }
        }
    }
}