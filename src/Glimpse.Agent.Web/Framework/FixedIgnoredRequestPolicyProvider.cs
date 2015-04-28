using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Agent.Web
{
    public class FixedIgnoredRequestPolicyProvider : IIgnoredRequestPolicyProvider
    {
        public FixedIgnoredRequestPolicyProvider()
            : this(Enumerable.Empty<IIgnoredRequestPolicy>())
        {
        }

        public FixedIgnoredRequestPolicyProvider(IEnumerable<IIgnoredRequestPolicy> controllerTypes)
        {
            Policies = new List<IIgnoredRequestPolicy>(controllerTypes);
        }
        
        public IList<IIgnoredRequestPolicy> Policies { get; }

        IEnumerable<IIgnoredRequestPolicy> IIgnoredRequestPolicyProvider.Policies => Policies;
    }
}