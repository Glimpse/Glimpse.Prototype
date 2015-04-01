using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Web
{
    public class FixedRequestHandlerProvider : IRequestHandlerProvider
    {
        public FixedRequestHandlerProvider()
            : this(Enumerable.Empty<IRequestHandler>())
        {
        }

        public FixedRequestHandlerProvider(IEnumerable<IRequestHandler> controllerTypes)
        {
            Handlers = new List<IRequestHandler>(controllerTypes);
        }

        public IList<IRequestHandler> Handlers { get; }

        IEnumerable<IRequestHandler> IRequestHandlerProvider.Handlers => Handlers;
    }
}