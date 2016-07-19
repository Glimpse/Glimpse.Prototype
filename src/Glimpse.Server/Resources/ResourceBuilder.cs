using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Server.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Glimpse.Server.Resources
{
    public class ResourceBuilder : IResourceBuilder
    {
        public ResourceBuilder(IApplicationBuilder app, IResourceManager resourceManager)
        {
            AppBuilder = app;
            ResourceManager = resourceManager;
        }
        private IResourceManager ResourceManager { get; }

        public IApplicationBuilder AppBuilder { get; }
        
        public IResourceBuilder Run(string name, string uriTemplate, ResourceType type, Func<HttpContext, IDictionary<string, string>, Task> resource)
        {
            ResourceManager.Register(name, uriTemplate, type, resource);

            return this;
        }

        public IResourceBuilder RegisterResource(string name, string uriTemplate)
        {
            ResourceManager.Register(name, uriTemplate);

            return this;
        }
    }
}