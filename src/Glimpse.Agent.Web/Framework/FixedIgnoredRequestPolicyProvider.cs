using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Agent.Web
{
    public class FixedIgnoredRequestPolicyProvider : IIgnoredRequestPolicyProvider
    {
        public FixedIgnoredRequestPolicyProvider()
            : this(Enumerable.Empty<IRequestIgnorePolicy>())
        {
        }

        public FixedIgnoredRequestPolicyProvider(IEnumerable<IRequestIgnorePolicy> controllerTypes)
        {
            Policies = new List<IRequestIgnorePolicy>(controllerTypes);
        }
        
        public IList<IRequestIgnorePolicy> Policies { get; }

        IEnumerable<IRequestIgnorePolicy> IIgnoredRequestPolicyProvider.Policies => Policies;
    }
}