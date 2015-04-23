using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Web
{
    public class FixedRequestRuntimeProvider : IRequestRuntimeProvider
    {
        public FixedRequestRuntimeProvider()
            : this(Enumerable.Empty<IRequestRuntime>())
        {
        }

        public FixedRequestRuntimeProvider(IEnumerable<IRequestRuntime> controllerTypes)
        {
            Runtimes = new List<IRequestRuntime>(controllerTypes);
        }

        public IList<IRequestRuntime> Runtimes { get; }

        IEnumerable<IRequestRuntime> IRequestRuntimeProvider.Runtimes => Runtimes;
    }
}