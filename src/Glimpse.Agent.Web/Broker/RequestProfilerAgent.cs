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

        public async Task Begin(IHttpContext newContext)
        { 
            var message = new BeginRequestMessage(newContext.Request);

            // TODO: Full out message more

            _messageBus.SendMessage(message);
        }

        public async Task End(IHttpContext newContext)
        { 
            var message = new EndRequestMessage(newContext.Request);

            // TODO: Full out message more

            _messageBus.SendMessage(message);
        }
    }
}