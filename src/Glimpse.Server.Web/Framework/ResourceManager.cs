using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Glimpse.Web;

namespace Glimpse.Server.Web
{
    public class ResourceManager : IResourceManager
    {
        private IDictionary<string, ResourceManagerItem> _resourceTable = new Dictionary<string, ResourceManagerItem>(StringComparer.OrdinalIgnoreCase);

        public void Register(string name, string uriTemplate,  Func<HttpContext, IDictionary<string, string>, Task> resource)
        {
            _resourceTable.Add(name, new ResourceManagerItem(uriTemplate, resource));
        }

        public ResourceManagerResult Match(HttpContext context)
        {
            var path = context.Request.Path;
            var remainingPath = (PathString)null;
            var startingSegment = path.StartingSegment(out remainingPath);
            var paramaters = (IDictionary<string, string>)null;
            var managerItem = (ResourceManagerItem)null;
            
            if (!string.IsNullOrEmpty(startingSegment)
                && _resourceTable.TryGetValue(startingSegment, out managerItem)
                && MatchUriTemplate(managerItem.UriTemplate, remainingPath, out paramaters))
            {
                return new ResourceManagerResult(paramaters, managerItem.Resource);
            }
            
            return null;
        }

        private bool MatchUriTemplate(string uriTemplate, PathString remainingPath, out IDictionary<string, string> paramaters)
        {
            paramaters = new Dictionary<string, string>();

            return true;
        }

        private class ResourceManagerItem
        {
            public ResourceManagerItem(string uriTemplate, Func<HttpContext, IDictionary<string, string>, Task> resource)
            {
                UriTemplate = uriTemplate;
                Resource = resource;
            }

            public string UriTemplate { get; }

            public Func<HttpContext, IDictionary<string, string>, Task> Resource { get; }
        }
    }
}