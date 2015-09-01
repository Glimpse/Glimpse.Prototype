using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Glimpse.Server.Web
{
    public class MessageHistoryResource : IResource
    {
        private readonly InMemoryStorage _store;
        private readonly JsonSerializer _jsonSerializer;

        public MessageHistoryResource(IStorage storage, JsonSerializer jsonSerializer)
        {
            // TODO: This hack is needed to get around signalr problem
            jsonSerializer.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // TODO: Really shouldn't be here 
            _store = (InMemoryStorage)storage;
            _jsonSerializer = jsonSerializer;
        }

        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            var response = context.Response;

            response.Headers[HeaderNames.ContentType] = "application/json";

            var list = _store.Query(null);
            var output = _jsonSerializer.Serialize(list);

            await response.WriteAsync(output);
        }

        public string Name => "MessageHistory";

        public ResourceParameters Parameters => null;
    }
}