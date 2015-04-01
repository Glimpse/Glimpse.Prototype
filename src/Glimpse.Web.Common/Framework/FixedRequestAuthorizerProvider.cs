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
            Authorizers = new List<IRequestAuthorizer>(controllerTypes);
        }
        
        public IList<IRequestAuthorizer> Authorizers { get; }

        IEnumerable<IRequestAuthorizer> IRequestAuthorizerProvider.Authorizers => Authorizers;
    }
}