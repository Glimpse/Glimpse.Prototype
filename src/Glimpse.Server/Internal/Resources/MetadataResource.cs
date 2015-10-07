using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Server.Configuration;
using Glimpse.Server.Resources;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Glimpse.Common.Internal.Extensions;

namespace Glimpse.Server.Internal.Resources
{
    public class MetadataResource : IResource
    {
        private readonly IMetadataProvider _metadataProvider;
        private readonly JsonSerializer _jsonSerializer;
        private Metadata _metadata;

        public MetadataResource(IMetadataProvider metadataProvider, JsonSerializer jsonSerializer)
        {
            _metadataProvider = metadataProvider;
            _jsonSerializer = jsonSerializer;
        }

        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            var metadata = GetMetadata();

            var response = context.Response;
            response.Headers[HeaderNames.ContentType] = "application/json";
            await response.WriteAsync(_jsonSerializer.Serialize(metadata));
        }

        private Metadata GetMetadata()
        {
            if (_metadata != null)
                return _metadata;

            _metadata = _metadataProvider.BuildInstance();
            return _metadata;
        }

        public string Name => "Metadata";
        public IEnumerable<ResourceParameter> Parameters => new [] { +ResourceParameter.Hash };
        public ResourceType Type => ResourceType.Client;
    }
}
