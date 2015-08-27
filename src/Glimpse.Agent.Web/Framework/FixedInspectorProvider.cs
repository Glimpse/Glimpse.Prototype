using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Agent.Web
{
    public class FixedInspectorProvider : IInspectorProvider
    {
        public FixedInspectorProvider()
            : this(Enumerable.Empty<IInspector>())
        {
        }

        public FixedInspectorProvider(IEnumerable<IInspector> controllerTypes)
        {
            Inspectors = new List<IInspector>(controllerTypes);
        }

        public IList<IInspector> Inspectors { get; }

        IEnumerable<IInspector> IInspectorProvider.Inspectors => Inspectors;
    }
}