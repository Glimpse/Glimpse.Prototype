using Glimpse.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glimpse.Agent.Web
{
    public class ProfilerRequestRuntimeHost : IRequestRuntime
    {
        private readonly IEnumerable<IRequestProfiler> _requestProfiliers;
        private readonly IEnumerable<IRequestIgnorePolicy> _requestIgnorePolicies;
        
        public ProfilerRequestRuntimeHost(IRequestProfilerProvider requestProfilerProvider, IIgnoredRequestPolicyProvider ignoredRequestPolicyProvider)
        {
            _requestProfiliers = requestProfilerProvider.Profilers; 
            _requestIgnorePolicies = ignoredRequestPolicyProvider.Policies;
        }

        public async Task Begin(IHttpContext context)
        {
            if (ShouldProfile(context))
            {
                foreach (var requestRuntime in _requestProfiliers)
                {
                    await requestRuntime.Begin(context);
                }
            }
        }

        public async Task End(IHttpContext context)
        {
            if (ShouldProfile(context))
            {
                foreach (var requestRuntime in _requestProfiliers)
                {
                    await requestRuntime.End(context);
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