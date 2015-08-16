using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Server.Web
{
    public class FixedMiddlewareResourceComposerProvider : IMiddlewareResourceComposerProvider
    {
        public FixedMiddlewareResourceComposerProvider()
            : this(Enumerable.Empty<IMiddlewareResourceComposer>())
        {
        }

        public FixedMiddlewareResourceComposerProvider(IEnumerable<IMiddlewareResourceComposer> resourceCollection)
        {
            Resources = new List<IMiddlewareResourceComposer>(resourceCollection);
        }

        public IList<IMiddlewareResourceComposer> Resources { get; }

        IEnumerable<IMiddlewareResourceComposer> IMiddlewareResourceComposerProvider.Resources
        {
            get { return Resources; }
        }
    }
}