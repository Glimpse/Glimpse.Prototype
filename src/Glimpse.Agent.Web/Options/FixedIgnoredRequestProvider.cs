using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Agent.Web.Options
{
    public class FixedIgnoredRequestProvider : IIgnoredRequestProvider
    {
        public FixedIgnoredRequestProvider()
            : this(Enumerable.Empty<IIgnoredRequestPolicy>())
        {
        }

        public FixedIgnoredRequestProvider(IEnumerable<IIgnoredRequestPolicy> controllerTypes)
        {
            Policies = new List<IIgnoredRequestPolicy>(controllerTypes);
        }
        
        public IList<IIgnoredRequestPolicy> Policies { get; }

        IEnumerable<IIgnoredRequestPolicy> IIgnoredRequestProvider.Policies => Policies;
    }
}