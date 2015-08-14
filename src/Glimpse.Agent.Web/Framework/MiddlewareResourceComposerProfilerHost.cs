using Glimpse.Web;
using Microsoft.AspNet.Http;
using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Agent.Web
{
    public class MiddlewareResourceComposerProfilerHost //: IMiddlewareResourceComposer
    {
        private readonly IEnumerable<IRequestProfiler> _requestProfiliers;
        private readonly IEnumerable<IRequestIgnorer> _requestIgnorePolicies;
        
        public MiddlewareResourceComposerProfilerHost(IRequestProfilerProvider requestProfilerProvider, IRequestIgnorerProvider requestIgnorerProvider)
        {
            _requestProfiliers = requestProfilerProvider.Profilers; 
            _requestIgnorePolicies = requestIgnorerProvider.Policies;
        }

        public void Begin(HttpContext context)
        {
            if (ShouldProfile(context))
            {
                foreach (var requestRuntime in _requestProfiliers)
                {
                    requestRuntime.Begin(context);
                }
            }
        }

        public void End(HttpContext context)
        {
            if (ShouldProfile(context))
            {
                foreach (var requestRuntime in _requestProfiliers)
                {
                    requestRuntime.End(context);
                }
            }
        }

        public bool ShouldProfile(HttpContext context)
        {
            if (_requestIgnorePolicies.Any())
            {
                foreach (var policy in _requestIgnorePolicies)
                {
                    if (policy.ShouldIgnore(context))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}