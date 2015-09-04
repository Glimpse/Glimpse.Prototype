using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Linq;
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