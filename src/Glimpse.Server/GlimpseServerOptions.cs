using System;
using System.Collections.Generic;
using Microsoft.AspNet.Http;

namespace Glimpse.Server
{
    public class GlimpseServerOptions
    {
        public bool AllowRemote { get; set; }

        public string BasePath { get; set; } = "glimpse";

        public Action<IDictionary<string, string>> OverrideResources { get; set; } = _ => { };

        public Func<HttpContext, bool> AllowClientAccess { get; set; }

        public Func<HttpContext, bool> AllowAgentAccess { get; set; }
    }
}