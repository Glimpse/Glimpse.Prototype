using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Web
{
    public class FixedRequestAuthorizerProvider : IRequestAuthorizerProvider
    {
        public FixedRequestAuthorizerProvider()
            : this(Enumerable.Empty<IRequestAuthorizer>())
        {
        }

        public FixedRequestAuthorizerProvider(IEnumerable<IRequestAuthorizer> controllerTypes)
        {
            Policies = new List<IRequestAuthorizer>(controllerTypes);
        }
        
        public IList<IRequestAuthorizer> Policies { get; }

        IEnumerable<IRequestAuthorizer> IRequestAuthorizerProvider.Authorizers => Policies;
    }
}