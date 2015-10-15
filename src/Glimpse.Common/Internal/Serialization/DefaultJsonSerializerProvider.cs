using Glimpse.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Glimpse.Common.Internal.Serialization
{
    public class DefaultJsonSerializerProvider : IJsonSerializerProvider
    {
        private readonly JsonSerializer _jsonSerializer;
        public DefaultJsonSerializerProvider(JsonSerializer jsonSerializer)
        {
            jsonSerializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSerializer.Converters.Add(new TimeSpanConverter());
            jsonSerializer.Converters.Add(new StringValuesConverter());
            jsonSerializer.Converters.Add(new GuidConverter());
            _jsonSerializer = jsonSerializer;
        }

        public JsonSerializer GetJsonSerializer() => _jsonSerializer;
    }
}