using System;
using Glimpse.Server.Resources;
using Glimpse.Server.Internal.Extensions;
using Glimpse.Server.Storage;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Internal.Resources
{
    public class RequestResource : IResourceStartup
    {
        private readonly IQueryRequests _requests;

        public RequestResource(IStorage storage)
        {
            if (!storage.Supports<IQueryRequests>())
                throw new ArgumentException($"IStorage implementation of type '{storage.GetType().FullName}' does not support IQueryRequests.", nameof(storage));

            _requests = storage.As<IQueryRequests>();
        }

        public void Configure(IResourceBuilder resourceBuilder)
        {
            resourceBuilder.Run("request", "{requestId}", ResourceType.Client, async (context, parameters) =>
            {
                var response = context.Response;
                var requestId = parameters.ParseGuid("requestId");

                if (!requestId.HasValue)
                {
                    response.StatusCode = 404;
                    await response.WriteAsync("Required parameter 'requestId' is missing.");
                    return;
                }

                var list = await _requests.GetByRequestId(requestId.Value);

                response.Headers[HeaderNames.ContentType] = "application/json";
                await response.WriteAsync(list.ToJsonArray());
            });
        }

        public ResourceType Type => ResourceType.Client;
    }
}
