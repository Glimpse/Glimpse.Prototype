using Glimpse.Web;
using System;
using System.Threading.Tasks;

namespace Glimpse.Agent.Web
{
    public class AgentRuntime : IRequestRuntime
    {
        private readonly string _requestIdKey = "RequestId";
        private readonly IMessageAgentBus _messageBus;

        public AgentRuntime(IMessageAgentBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task Begin(IContext newContext)
        {
            var requestId = Guid.NewGuid();

            newContext.Items.Add(_requestIdKey, requestId);

            var message = new BeginRequestMessage(requestId, newContext.Request);

            // TODO: Full out message more

            await _messageBus.SendMessage(message);
        }

        public async Task End(IContext newContext)
        {
            var requestId = (Guid)newContext.Items[_requestIdKey];

            var message = new EndRequestMessage(requestId, newContext.Request);

            // TODO: Full out message more

            await _messageBus.SendMessage(message);
        }
    }
}