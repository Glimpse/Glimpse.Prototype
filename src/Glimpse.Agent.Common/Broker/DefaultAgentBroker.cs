using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Glimpse.Agent
{ 
    public class DefaultAgentBroker : IAgentBroker
    {
        private readonly IChannelSender _channelSender;
        private readonly IMessageConverter _messageConverter;
        private readonly ISubject<MessageListenerOptions> _subject;
        private readonly BlockingCollection<IMessage> _queue;

        // TODO: Review if we care about unifying which thread message is published on
        //       and which thread it is recieved on. If so need to use IScheduler.

        public DefaultAgentBroker(IChannelSender channelSender, IMessageConverter messageConverter)
        {
            _channelSender = channelSender;
            _messageConverter = messageConverter;
            _subject = new BehaviorSubject<MessageListenerOptions>(null);
            _queue = new BlockingCollection<IMessage>();
             
            Task.Run(() => ReadMessages());
        }

        private void ReadMessages()
        {
            foreach (var message in _queue.GetConsumingEnumerable())
            {
                // run through all listeners
                var notificationOptions = new MessageListenerOptions(message);

                 _subject.OnNext(notificationOptions);

                if (!notificationOptions.IsCancelled)
                {
                    _channelSender.PublishMessage(message);
                }
            }
        }

        public IObservable<MessageListenerOptions> Listen<T>() 
        {
            return ListenIncludeLatest<T>().Skip(1);
        }

        public IObservable<MessageListenerOptions> ListenIncludeLatest<T>() 
        {
            return _subject
                .Where(opts => typeof(T).GetTypeInfo().IsAssignableFrom(opts.Message.Payload.GetType().GetTypeInfo()));
        }
        public IObservable<MessageListenerOptions> ListenAll()
        {
            return ListenAllIncludeLatest().Skip(1);
        }

        public IObservable<MessageListenerOptions> ListenAllIncludeLatest()
        {
            return _subject;
        }

        public void SendMessage(object payload)
        {
            var message = _messageConverter.ConvertMessage(payload);

            _queue.Add(message);
        }
    }
}