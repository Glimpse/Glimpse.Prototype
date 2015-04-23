using Glimpse.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glimpse.Agent.Web
{
    public class MasterRequestProfiler : IRequestRuntime
    {
        private readonly IEnumerable<IRequestProfiler> _requestProfiliers;
        private readonly IEnumerable<IIgnoredRequestPolicy> _ignoredRequestPolicies;

        public MasterRequestProfiler(IRequestProfilerProvider requestProfilerProvider, IIgnoredRequestProvider ignoredRequestProvider)
        {
            _requestProfiliers = requestProfilerProvider.Profilers; 
            _ignoredRequestPolicies = ignoredRequestProvider.Policies;
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
            if (_ignoredRequestPolicies.Any())
            {
                foreach (var policy in _ignoredRequestPolicies)
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