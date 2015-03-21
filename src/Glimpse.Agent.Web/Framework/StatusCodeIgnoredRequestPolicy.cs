using System.Collections.Generic;
using Glimpse.Web;
using Microsoft.Framework.OptionsModel;

namespace Glimpse.Agent.Web.Framework
{
    public class StatusCodeIgnoredRequestPolicy : IIgnoredRequestPolicy
    {
        private readonly IList<int> _statusCodes;

        public StatusCodeIgnoredRequestPolicy(IOptions<GlimpseAgentWebOptions> optionsAccessor)
        {
            _statusCodes = optionsAccessor.Options.StatusCodes;
        }

        public bool ShouldIgnore(IHttpContext context)
        {
            return _statusCodes.Contains(context.Response.StatusCode);
        }
    }
}