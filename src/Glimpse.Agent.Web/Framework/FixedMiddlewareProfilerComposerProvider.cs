using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Agent.Web
{
    public class FixedMiddlewareProfilerComposerProvider : IMiddlewareProfilerComposerProvider
    {
        public FixedMiddlewareProfilerComposerProvider()
            : this(Enumerable.Empty<IMiddlewareProfilerComposer>())
        {
        }

        public FixedMiddlewareProfilerComposerProvider(IEnumerable<IMiddlewareProfilerComposer> controllerTypes)
        {
            Profilers = new List<IMiddlewareProfilerComposer>(controllerTypes);
        }

        public IList<IMiddlewareProfilerComposer> Profilers { get; }

        IEnumerable<IMiddlewareProfilerComposer> IMiddlewareProfilerComposerProvider.Profilers => Profilers;
    }
}