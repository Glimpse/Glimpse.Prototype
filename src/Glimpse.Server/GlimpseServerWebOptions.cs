using System;
using Microsoft.AspNet.Http;

namespace Glimpse.Server
{
    public class GlimpseServerWebOptions
    {
        public bool AllowRemote { get; set; }

        public Func<HttpContext, bool> AllowClientAccess { get; set; }

        public Func<HttpContext, bool> AllowAgentAccess { get; set; }
    }
}