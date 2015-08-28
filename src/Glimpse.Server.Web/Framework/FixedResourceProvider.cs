using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Server.Web
{
    public class FixedResourceProvider : IResourceProvider
    {
        public FixedResourceProvider()
            : this(Enumerable.Empty<IResource>())
        {
        }

        public FixedResourceProvider(IEnumerable<IResource> resourceCollection)
        {
            Resources = new List<IResource>(resourceCollection);
        }

        public IList<IResource> Resources { get; }

        IEnumerable<IResource> IResourceProvider.Resources
        {
            get { return Resources; }
        }
    }
}