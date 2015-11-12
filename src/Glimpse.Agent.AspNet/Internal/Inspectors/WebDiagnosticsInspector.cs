using Glimpse.Agent.Internal.Inspectors.Mvc.Proxies;
using Glimpse.Internal;

namespace Glimpse.Agent.Internal.Inspectors
{
    // NOTE: This has been setup as a partial class to prevent having multiple 
    //       listeners registered. Under the covers, Diagnostics Source will 
    //       loop through all listeners, only have the one registed should 
    //       minimise how much we have to have registered.
    public partial class WebDiagnosticsInspector
    {
        private readonly IAgentBroker _broker;
        private readonly IContextData<MessageContext> _contextData;
        private readonly ProxyAdapter _proxyAdapter;
        private readonly IExceptionProcessor _exceptionProcessor;

        public WebDiagnosticsInspector(IExceptionProcessor exceptionProcessor, IAgentBroker broker, IContextData<MessageContext> contextData)
        {
            _exceptionProcessor = exceptionProcessor;
            _broker = broker;
            _contextData = contextData;

            _proxyAdapter = new ProxyAdapter();

            AspNetOnCreated();
            MvcOnCreated();
        }

        partial void AspNetOnCreated();
        partial void MvcOnCreated();
    }
}
