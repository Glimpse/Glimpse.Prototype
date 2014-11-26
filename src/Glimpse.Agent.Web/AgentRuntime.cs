using Glimpse.Web;
using System;

namespace Glimpse.Agent.Web
{
    public class AgentRuntime : IRequestRuntime
    {
        private readonly IMessageAgentBus _messageBus;

        public AgentRuntime(IMessageAgentBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Begin(IContext newContext)
        {
            var message = new BeginRequestMessage();

            // TODO: Full out message more

            _messageBus.SendMessage(message);
        }

        public void End(IContext newContext)
        {
            var message = new EndRequestMessage();

            // TODO: Full out message more

            _messageBus.SendMessage(message);
        }
    }
}