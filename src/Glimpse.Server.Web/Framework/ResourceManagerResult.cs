using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Glimpse.Server.Web
{
    public class ResourceManagerResult
    {
        public ResourceManagerResult(IDictionary<string, string> paramaters, Func<HttpContext, IDictionary<string, string>, Task> resource)
        {
            Paramaters = paramaters;
            Resource = resource;
        }

        public IDictionary<string, string> Paramaters { get; }

        public Func<HttpContext, IDictionary<string, string>, Task> Resource { get; }
    }
}