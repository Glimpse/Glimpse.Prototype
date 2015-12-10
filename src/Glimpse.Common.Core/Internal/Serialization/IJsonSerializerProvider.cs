using Newtonsoft.Json;

namespace Glimpse.Common.Internal.Serialization
{
    public interface IJsonSerializerProvider
    {
        JsonSerializer GetJsonSerializer();
    }
}
