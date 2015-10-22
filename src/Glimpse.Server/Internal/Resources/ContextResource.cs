using System.Linq;
using Glimpse.Server.Resources;
using Glimpse.Server.Internal.Extensions;
using Glimpse.Server.Storage;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Internal.Resources
{
    public class ContextResource : IResourceStartup
    {
        private readonly IStorage _storage;

        public ContextResource(IStorage storage)
        {
            _storage = storage;
        }

        public void Configure(IResourceBuilder resourceBuilder)
        {
            resourceBuilder.Run("context", "?contextId={contextId}{&types}", ResourceType.Client, async (context, parameters) =>
            {
                var response = context.Response;
                var contextId = parameters.ParseGuid("contextId");

                if (!contextId.HasValue)
                {
                    response.StatusCode = 404;
                    await response.WriteAsync("Required parameter 'contextId' is missing.");
                    return;
                }

                var types = parameters.ParseEnumerable("types").ToArray();

               var list = await _storage.RetrieveByContextId(contextId.Value, types);

                response.Headers[HeaderNames.ContentType] = "application/json";
                await response.WriteAsync(list.ToJsonArray());
            });
        }

        public ResourceType Type => ResourceType.Client;
    }
}
