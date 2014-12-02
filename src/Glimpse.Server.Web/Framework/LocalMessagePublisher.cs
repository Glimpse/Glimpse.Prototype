using Glimpse.Agent;
using System;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    public class LocalMessagePublisher : BaseMessagePublisher
    {
        private readonly IMessageServerBus _messageBus;

        public LocalMessagePublisher(IMessageServerBus messageBus)
        {
            _messageBus = messageBus;
        }

        public override async Task PublishMessage(IMessage message)
        {
            var newMessage = ConvertMessage(message);

            await _messageBus.SendMessage(newMessage);
        }
    }
}