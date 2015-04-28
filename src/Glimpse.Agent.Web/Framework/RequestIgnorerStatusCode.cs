using System.Collections.Generic;
using System.Linq;
using Glimpse.Agent.Web;
using Glimpse.Web;
using Microsoft.Framework.OptionsModel;

namespace Glimpse.Agent.Web.Framework
{
    public class RequestIgnorerStatusCode : IRequestIgnorer
    {
        private readonly IReadOnlyCollection<int> _statusCodes;

        public RequestIgnorerStatusCode(IRequestIgnorerStatusCodeProvider requestIgnorerStatusCodeProvider)
        {
            _statusCodes = requestIgnorerStatusCodeProvider.StatusCodes;
        }

        public bool ShouldIgnore(IHttpContext context)
        {
            return _statusCodes.Contains(context.Response.StatusCode);
        }
    }
}