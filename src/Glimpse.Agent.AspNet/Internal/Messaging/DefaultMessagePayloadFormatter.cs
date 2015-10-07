using System.Linq;
using Glimpse.Internal;
using Glimpse.Internal.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Glimpse.Agent.Internal.Messaging
{
    public class DefaultMessagePayloadFormatter : IMessagePayloadFormatter
    {
        private readonly JsonSerializer _jsonSerializer;

        public DefaultMessagePayloadFormatter(JsonSerializer jsonSerializer)
        {
            jsonSerializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSerializer.Converters.Add(new TimeSpanConverter());
            jsonSerializer.Converters.Add(new StringValuesConverter());

            _jsonSerializer = jsonSerializer;
        }
        
        public virtual string Serialize(IMessage message, object payload)
        {
            var content = SerializeCore(payload);

            // NOTE: Manually seralize things here as we can be 
            //       faster for what we need to do
            var messageTypes = "[" + string.Join(",", message.Types.Select(s => "\"" + s + "\"")) + "]";
            var messageContext = $"{{\"id\":\"{message.Context.Id}\",\"type\":\"{message.Context.Type}\"}}";

            return $"{{\"id\":\"{message.Id}\",\"ordinal\":\"{message.Ordinal}\",\"types\":{messageTypes},\"payload\":{content},\"context\":{messageContext}}}";
        }

        public virtual string SerializeCore(object payload)
        {
            return _jsonSerializer.Serialize(payload);
        }
    }
}