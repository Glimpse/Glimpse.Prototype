using Glimpse.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glimpse.Agent.Web
{
    public class RequestRuntimeProfilerHost : IRequestRuntime
    {
        private readonly IEnumerable<IRequestProfiler> _requestProfiliers;
        private readonly IEnumerable<IRequestIgnorer> _requestIgnorePolicies;
        
        public RequestRuntimeProfilerHost(IRequestProfilerProvider requestProfilerProvider, IRequestIgnorerProvider requestIgnorerProvider)
        {
            _requestProfiliers = requestProfilerProvider.Profilers; 
            _requestIgnorePolicies = requestIgnorerProvider.Policies;
        }

        public void Begin(IHttpContext context)
        {
            if (ShouldProfile(context))
            {
                foreach (var requestRuntime in _requestProfiliers)
                {
                    requestRuntime.Begin(context);
                }
            }
        }

        public void End(IHttpContext context)
        {
            if (ShouldProfile(context))
            {
                foreach (var requestRuntime in _requestProfiliers)
                {
                    requestRuntime.End(context);
                }
            }
        }

        public bool ShouldProfile(IHttpContext context)
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