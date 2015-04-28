using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Agent.Web
{
    public class FixedRequestIgnorerProvider : IRequestIgnorerProvider
    {
        public FixedRequestIgnorerProvider()
            : this(Enumerable.Empty<IRequestIgnorer>())
        {
        }

        public FixedRequestIgnorerProvider(IEnumerable<IRequestIgnorer> controllerTypes)
        {
            Policies = new List<IRequestIgnorer>(controllerTypes);
        }
        
        public IList<IRequestIgnorer> Policies { get; }

        IEnumerable<IRequestIgnorer> IRequestIgnorerProvider.Policies => Policies;
    }
}