using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Server.Web
{
    public class FixedResourceStartupProvider : IResourceStartupProvider
    {
        public FixedResourceStartupProvider()
            : this(Enumerable.Empty<IResourceStartup>())
        {
        }

        public FixedResourceStartupProvider(IEnumerable<IResourceStartup> logicCollection)
        {
            Startups = new List<IResourceStartup>(logicCollection);
        }

        public IList<IResourceStartup> Startups { get; }

        IEnumerable<IResourceStartup> IResourceStartupProvider.Startups
        {
            get { return Startups; }
        }
    }
}