using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glimpse.Extensions;
using Glimpse.Server.Resources;
using Microsoft.AspNet.Http;

namespace Glimpse.Server.Internal
{
    public class ResourceManager : IResourceManager
    {
        private IDictionary<string, ResourceManagerItem> _resourceTable = new Dictionary<string, ResourceManagerItem>(StringComparer.OrdinalIgnoreCase);

        public void Register(string name, string uriTemplate, ResourceType type, Func<HttpContext, IDictionary<string, string>, Task> resource)
        {
            // todo: blow up on bad name??
            _resourceTable.Add(name, new ResourceManagerItem(type, uriTemplate, resource));
        }

        public ResourceManagerResult Match(HttpContext context)
        {
            var path = context.Request.Path;
            var remainingPath = (PathString)null;
            var startingSegment = path.StartingSegment(out remainingPath);
            var parameters = (IDictionary<string, string>)null;
            var managerItem = (ResourceManagerItem)null;
            
            if (!string.IsNullOrEmpty(startingSegment)
                && _resourceTable.TryGetValue(startingSegment, out managerItem)
                && MatchUriTemplate(managerItem.UriTemplate, remainingPath, out parameters))
            {
                return new ResourceManagerResult(parameters, managerItem.Resource, managerItem.Type);
            }
            
            return null;
        }

        public IReadOnlyDictionary<string, string> RegisteredUris
        {
            get { return _resourceTable.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.UriTemplate); }
        }

        private bool MatchUriTemplate(string uriTemplate, PathString remainingPath, out IDictionary<string, string> paramaters)
        {
            paramaters = new Dictionary<string, string>();

            return true;
        }

        private class ResourceManagerItem
        {
            public ResourceManagerItem(ResourceType type, string uriTemplate, Func<HttpContext, IDictionary<string, string>, Task> resource)
            {
                Type = type;
                UriTemplate = uriTemplate;
                Resource = resource;
            }

            public ResourceType Type { get; set; }

            public string UriTemplate { get; }

            public Func<HttpContext, IDictionary<string, string>, Task> Resource { get; }
        }
    }
}