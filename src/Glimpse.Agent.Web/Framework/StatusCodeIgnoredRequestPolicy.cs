using System.Collections.Generic;
using System.Linq;
using Glimpse.Agent.Web.Options;
using Glimpse.Web;
using Microsoft.Framework.OptionsModel;

namespace Glimpse.Agent.Web.Framework
{
    public class StatusCodeIgnoredRequestPolicy : IIgnoredRequestPolicy
    {
        private readonly IReadOnlyCollection<int> _statusCodes;

        public StatusCodeIgnoredRequestPolicy(IIgnoredStatusCodeProvider ignoredStatusCodeProvider)
        {
            _statusCodes = ignoredStatusCodeProvider.StatusCodes;
        }

        public bool ShouldIgnore(IHttpContext context)
        {
            return _statusCodes.Contains(context.Response.StatusCode);
        }
    }
}