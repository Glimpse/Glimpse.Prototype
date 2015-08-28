using System;
using System.Collections.Generic;

namespace Glimpse.Server.Web
{
    public class ResourcesResourceStartup : IResourceStartup
    {
        private readonly IEnumerable<IResource> _resources;

        public ResourcesResourceStartup(IResourceProvider resourceProvider)
        {
            _resources = resourceProvider.Resources;
        }
        
        public void Configure(IResourceBuilder builder)
        {
            foreach (var resource in _resources)
            {
                builder.Run(resource.Name, resource.Parameters?.GenerateUriTemplate(), resource.Invoke);
            }
        }
    }
}
