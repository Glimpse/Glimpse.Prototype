using Glimpse.Web;
using Microsoft.Framework.Logging;
using System;
using System.Threading.Tasks;

namespace Glimpse.Agent.Web
{
    public class RequestProfilerAgent : IRequestProfiler
    {
        private readonly string _requestIdKey = "RequestId";
        private readonly IAgentBroker _messageBus;

        public RequestProfilerAgent(IAgentBroker messageBus)
        {
            _messageBus = messageBus;
        }

        public void Begin(IHttpContext newContext)
        { 
            var message = new BeginRequestMessage(newContext.Request);

            // TODO: Full out message more

            _messageBus.BeginLogicalOperation(message);
        }

        public void End(IHttpContext newContext)
        {
            var timing = _messageBus.EndLogicalOperation<BeginRequestMessage>().Timing;
            var message = new EndRequestMessage(newContext.Request, timing);

            // TODO: Full out message more

            _messageBus.SendMessage(message);
        }
    }
}