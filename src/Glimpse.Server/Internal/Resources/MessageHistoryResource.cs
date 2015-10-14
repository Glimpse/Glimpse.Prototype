using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glimpse.Server.Resources;
using Glimpse.Server.Storage;
using Glimpse.Server.Internal.Extensions;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Internal.Resources
{
    public class MessageHistoryResource : IResource
    {
        private readonly IStorage _store;

        public MessageHistoryResource(IStorage storage)
        {
            _store = storage;
        }

        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            var response = context.Response;

            var types = parameters.ParseEnumerable("types").ToArray();

            if (types.Length == 0)
            {
                response.StatusCode = 404;
                await response.WriteAsync("Required parameter 'types' is missing.");
                return;
            }

            var list = await _store.RetrieveByType(types);

            response.Headers[HeaderNames.ContentType] = "application/json";
            await response.WriteAsync(list.ToJsonArray());
        }

        public string Name => "message-history";
        
        public IEnumerable<ResourceParameter> Parameters => new[] { +ResourceParameter.Custom("types") };

        public ResourceType Type => ResourceType.Client;
    }
}