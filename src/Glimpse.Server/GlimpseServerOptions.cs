using System;
using Microsoft.AspNet.Http;

namespace Glimpse.Server
{
    public class GlimpseServerOptions
    {
        public bool AllowRemote { get; set; }

        public string BasePath { get; set; } = "glimpse";

        public Func<HttpContext, bool> AllowClientAccess { get; set; }

        public Func<HttpContext, bool> AllowAgentAccess { get; set; }
    }
}