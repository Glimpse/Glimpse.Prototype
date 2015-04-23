using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Agent.Web
{
    public class FixedRequestProfilerProvider : IRequestProfilerProvider
    {
        public FixedRequestProfilerProvider()
            : this(Enumerable.Empty<IRequestProfiler>())
        {
        }

        public FixedRequestProfilerProvider(IEnumerable<IRequestProfiler> controllerTypes)
        {
            Profilers = new List<IRequestProfiler>(controllerTypes);
        }

        public IList<IRequestProfiler> Profilers { get; }

        IEnumerable<IRequestProfiler> IRequestProfilerProvider.Profilers => Profilers;
    }
}