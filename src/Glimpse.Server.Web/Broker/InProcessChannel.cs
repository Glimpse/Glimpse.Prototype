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
        private readonly IMessageConverter _messageConverter;
        private readonly IServerBroker _messageBus;
        private readonly ISubject<MessageListenerPayload> _listenerSubject;

        public InProcessChannel(IServerBroker messageBus, IMessageConverter messageConverter)
        {
            _messageBus = messageBus;
            _messageConverter = messageConverter;
            _listenerSubject = new Subject<MessageListenerPayload>();

            // ensure off-request message transport is obsered onto a different thread 
            _listenerSubject.Subscribe(x => Observable.Start(() => Process(x), TaskPoolScheduler.Default));
        }

        public void PublishMessage(MessageListenerPayload payload)
        {
            _listenerSubject.OnNext(payload);
        }

        private void Process(MessageListenerPayload payload)
        {
            var messages = _messageConverter.ConvertMessage(payload.Payload, payload.Context);
            _messageBus.SendMessage(messages);
        }
    }
}