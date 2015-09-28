using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;

namespace Glimpse.Agent.Web
{
    public class DefaultRequestIgnorerManager : IRequestIgnorerManager
    {
        public DefaultRequestIgnorerManager(IExtensionProvider<IRequestIgnorer> requestIgnorerProvider, IHttpContextAccessor httpContextAccessor)
        {
            RequestIgnorers = requestIgnorerProvider.Instances;
            HttpContextAccessor = httpContextAccessor;
        }

        private IEnumerable<IRequestIgnorer> RequestIgnorers { get; }

        private IHttpContextAccessor HttpContextAccessor { get; }

        public bool ShouldIgnore(HttpContext context)
        {
            if (RequestIgnorers.Any())
            {
                foreach (var policy in RequestIgnorers)
                {
                    if (policy.ShouldIgnore(GetContext(context)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private HttpContext GetContext(HttpContext context)
        {
            return context != null ? context : HttpContextAccessor.HttpContext;
        }
    }
}