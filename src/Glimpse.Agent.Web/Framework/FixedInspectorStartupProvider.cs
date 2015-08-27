using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Agent.Web
{
    public class FixedInspectorStartupProvider : IInspectorStartupProvider
    {
        public FixedInspectorStartupProvider()
            : this(Enumerable.Empty<IInspectorStartup>())
        {
        }

        public FixedInspectorStartupProvider(IEnumerable<IInspectorStartup> controllerTypes)
        {
            Startups = new List<IInspectorStartup>(controllerTypes);
        }

        public IList<IInspectorStartup> Startups { get; }

        IEnumerable<IInspectorStartup> IInspectorStartupProvider.Startups => Startups;
    }
}