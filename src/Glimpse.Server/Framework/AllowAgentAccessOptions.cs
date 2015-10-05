using System;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.OptionsModel;

namespace Glimpse.Server.Web.Framework
{
    public class AllowAgentAccessOptions : IAllowAgentAccess
    {
        private readonly Func<HttpContext, bool> _allowAgentAccess;

        public AllowAgentAccessOptions(IOptions<GlimpseServerWebOptions> optionsAccessor)
        {
            _allowAgentAccess = optionsAccessor.Value.AllowAgentAccess;
        }

        public bool AllowAgent(HttpContext context)
        {
            return _allowAgentAccess != null ? _allowAgentAccess(context) : true;
        }
    }
}
