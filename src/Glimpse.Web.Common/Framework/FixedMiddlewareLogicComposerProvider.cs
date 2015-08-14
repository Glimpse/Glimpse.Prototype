using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Web
{
    public class FixedMiddlewareLogicComposerProvider : IMiddlewareLogicComposerProvider
    {
        public FixedMiddlewareLogicComposerProvider()
            : this(Enumerable.Empty<IMiddlewareLogicComposer>())
        {
        }

        public FixedMiddlewareLogicComposerProvider(IEnumerable<IMiddlewareLogicComposer> logicCollection)
        {
            Logic = new List<IMiddlewareLogicComposer>(logicCollection);
        }

        public IList<IMiddlewareLogicComposer> Logic { get; }

        IEnumerable<IMiddlewareLogicComposer> IMiddlewareLogicComposerProvider.Logic
        {
            get { return Logic; }
        }
    }
}