using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace Glimpse.Server.Resources
{
    public interface IResourceBuilder
    {
        IApplicationBuilder AppBuilder { get; }
        
        IResourceBuilder Run(string name, string uriTemplate, ResourceType type, Func<HttpContext, IDictionary<string, string>, Task> resource);
    }
}