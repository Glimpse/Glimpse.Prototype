using System.Linq;
using Glimpse.Server.Internal;
using Glimpse.Internal.Extensions;
using Microsoft.AspNet.Http;

namespace Glimpse.Server.Configuration
{
    public class DefaultMetadataProvider : IMetadataProvider
    {
        private readonly IResourceManager _resourceManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Metadata _metadata;

        public DefaultMetadataProvider(IResourceManager resourceManager, IHttpContextAccessor httpContextAccessor)
        {
            _resourceManager = resourceManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public Metadata BuildInstance()
        {
            if (_metadata != null)
                return _metadata;

            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}/glimpse/"; //TODO: 'glimpse' should come from config
            var resources = _resourceManager.RegisteredUris.ToDictionary(kvp => kvp.Key.KebabCase(), kvp => $"{baseUrl}{kvp.Key}/{kvp.Value}");

            _metadata = new Metadata(resources);

            // TODO: Where to call user func?

            return _metadata;
        }
    }
}