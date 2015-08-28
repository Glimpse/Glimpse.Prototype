using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace Glimpse.Server.Web
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
        
        public IResourceBuilder Run(string name, string uriTemplate, Func<HttpContext, IDictionary<string, string>, Task> resource)
        {
            ResourceManager.Register(name, uriTemplate, resource);

            return this;
        }
    }
}