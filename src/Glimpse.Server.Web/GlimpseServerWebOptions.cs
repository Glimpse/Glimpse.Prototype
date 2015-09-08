using System;
using Microsoft.AspNet.Http;

namespace Glimpse.Server.Web
{
    public class GlimpseServerWebOptions
    {
        public bool AllowRemote { get; set; }

        public Func<HttpContext, bool> CanAccessClient { get; set; }

        public Func<HttpContext, bool> ShouldAllowAgent { get; set; }
    }
}