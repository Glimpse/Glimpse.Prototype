using Glimpse.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glimpse.Agent.Web.Options;

namespace Glimpse.Agent.Web
{
    public class MasterRequestProfiler : IRequestRuntime
    {
        private readonly IDiscoverableCollection<IRequestProfiler> _requestProfiliers;
        private readonly IEnumerable<IIgnoredRequestPolicy> _ignoredRequestPolicies;

        public MasterRequestProfiler(IDiscoverableCollection<IRequestProfiler> requestProfiliers, IIgnoredRequestProvider ignoredRequestProvider)
        {
            _requestProfiliers = requestProfiliers;
            _requestProfiliers.Discover();

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
        
        public bool ShouldProfileOld(IHttpContext context)
        {
            // TODO: confirm that we want on by default. I'm
            //       thinking yes since they can easily exclude
            //       Glimpse middleware if they want.

            // TODO: Propbably should be hardcoded to ignore
            //       own requests here.

            // TODO: Hack for favicon atm. Can't be here

            if (context.Request.Path.IndexOf("/Glimpse", StringComparison.OrdinalIgnoreCase) > -1
                || context.Request.Path.IndexOf("favicon.ico", StringComparison.OrdinalIgnoreCase) > -1)
            {
                return false;
            }
            else if (context.Settings.ShouldProfile != null)
            {
                return context.Settings.ShouldProfile(context);
            }

            return true;
        }
    }
}