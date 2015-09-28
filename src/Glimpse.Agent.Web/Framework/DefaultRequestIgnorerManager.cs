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

        public bool ShouldIgnore()
        {
            return ShouldIgnore(HttpContextAccessor.HttpContext);
        }

        public bool ShouldIgnore(HttpContext context)
        {
            if (RequestIgnorers.Any())
            {
                foreach (var policy in RequestIgnorers)
                {
                    if (policy.ShouldIgnore(context))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}