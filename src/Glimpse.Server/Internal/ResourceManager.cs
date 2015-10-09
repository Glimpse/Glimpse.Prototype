using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Glimpse.Server.Resources;
using Microsoft.AspNet.Http;
using Glimpse.Internal.Extensions;

namespace Glimpse.Server.Internal
{
    public class ResourceManager : IResourceManager
    {
        private IDictionary<string, string> _templateTable = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private IDictionary<string, ResourceManagerItem> _resourceTable = new Dictionary<string, ResourceManagerItem>(StringComparer.OrdinalIgnoreCase);

        public void Register(string name, string uriTemplate)
        {
            _templateTable.Add(name, uriTemplate);
        }

        public void Register(string name, string uriTemplate, ResourceType type, Func<HttpContext, IDictionary<string, string>, Task> resource)
        {
            _resourceTable.Add(name, new ResourceManagerItem(type, uriTemplate, resource));
            Register(name, uriTemplate);
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

        public IReadOnlyDictionary<string, string> RegisteredUris => new ReadOnlyDictionary<string, string>(_templateTable);

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