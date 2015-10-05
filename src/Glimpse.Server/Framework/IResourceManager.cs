using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Glimpse.Server.Web
{
    public interface IResourceManager
    {
        void Register(string name, string uriTemplate, ResourceType type, Func<HttpContext, IDictionary<string, string>, Task> resource);

        ResourceManagerResult Match(HttpContext context);

        IReadOnlyDictionary<string, string> RegisteredUris { get; }
    }
}