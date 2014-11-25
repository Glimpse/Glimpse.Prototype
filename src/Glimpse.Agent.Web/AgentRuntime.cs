using Glimpse.Web;
using System;

namespace Glimpse.Agent.Web
{
    public class AgentRuntime : IRequestRuntime
    {
        private readonly IMessagePublisher _messagePublisher;

        public AgentRuntime(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        public void Begin(IContext newContext)
        {
            var message = new BeginRequestMessage();

            // TODO: Full out message more

            _messagePublisher.PublishMessage(message);
        }

        public void End(IContext newContext)
        {
            var message = new EndRequestMessage();

            // TODO: Full out message more

            _messagePublisher.PublishMessage(message);
        }
    }
}