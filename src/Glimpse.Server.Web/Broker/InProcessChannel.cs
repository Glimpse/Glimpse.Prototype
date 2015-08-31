using Glimpse.Agent;

namespace Glimpse.Server.Web
{
    public class InProcessChannel : IMessagePublisher
    {
        private readonly IServerBroker _messageBus;

        public InProcessChannel(IServerBroker messageBus)
        {
            _messageBus = messageBus;
        }

        public void PublishMessage(IMessage message)
        {
            _messageBus.SendMessage(message);
        }
    }
}