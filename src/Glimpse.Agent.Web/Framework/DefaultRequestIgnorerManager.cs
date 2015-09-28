using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;

namespace Glimpse.Agent.Web
{
    public class DefaultRequestIgnorerManager : IRequestIgnorerManager
    {
        public DefaultRequestIgnorerManager(IExtensionProvider<IRequestIgnorer> requestIgnorerProvider)
        {
            RequestIgnorers = requestIgnorerProvider.Instances;
        }

        private IEnumerable<IRequestIgnorer> RequestIgnorers { get; }

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