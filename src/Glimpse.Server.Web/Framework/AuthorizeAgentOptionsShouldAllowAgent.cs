using System;
using Microsoft.AspNet.Http;
using Microsoft.Framework.OptionsModel;

namespace Glimpse.Server.Web.Framework
{
    public class AuthorizeAgentOptionsShouldAllowAgent : IAuthorizeAgent
    {
        private readonly Func<HttpContext, bool> _shouldAllowAgent;

        public AuthorizeAgentOptionsShouldAllowAgent(IOptions<GlimpseServerWebOptions> optionsAccessor)
        {
            _shouldAllowAgent = optionsAccessor.Value.ShouldAllowAgent;
        }

        public bool AllowAgent(HttpContext context)
        {
            return _shouldAllowAgent != null ? _shouldAllowAgent(context) : true;
        }
    }
}
