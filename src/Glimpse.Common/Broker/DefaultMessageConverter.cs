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
        private readonly IServiceProvider _serviceProvider;

        public DefaultMessageConverter(JsonSerializer jsonSerializer, IServiceProvider serviceProvider)
        {
            _jsonSerializer = jsonSerializer;
            _serviceProvider = serviceProvider;
        }

        public IMessageEnvelope ConvertMessage(IMessage message)
        {
            //var context = (MessageContext)_serviceProvider.GetService(typeof(MessageContext));

            var newMessage = new MessageEnvelope();
            newMessage.Type = message.GetType().FullName;
            newMessage.Payload = Serialize(message);
            //newMessage.Context = context;
            
            return newMessage;
        }

        protected string Serialize(object data)
        {
            // Brought across from - https://github.com/JamesNK/Newtonsoft.Json/blob/master/Src/Newtonsoft.Json/JsonConvert.cs#L635
            var stringBuilder = new StringBuilder(256);
            using (var stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
            using (var jsonWriter = new JsonTextWriter(stringWriter))
            {
                _jsonSerializer.Serialize(jsonWriter, data, data.GetType());

                return stringWriter.ToString();
            }
        }
    }
}