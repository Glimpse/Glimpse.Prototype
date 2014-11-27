using Glimpse.Web;
using System;

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

        public void Begin(IContext newContext)
        {
            var requestId = Guid.NewGuid();

            newContext.Items.Add(_requestIdKey, requestId);

            var message = new BeginRequestMessage(requestId);

            // TODO: Full out message more

            _messageBus.SendMessage(message);
        }

        public void End(IContext newContext)
        {
            var requestId = (Guid)newContext.Items[_requestIdKey];

            var message = new EndRequestMessage(requestId);

            // TODO: Full out message more

            _messageBus.SendMessage(message);
        }
    }
}