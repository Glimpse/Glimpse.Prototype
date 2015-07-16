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
        private readonly ISubject<IMessageEnvelope> _subject;
        private readonly BlockingCollection<IMessageEnvelope> _queue;

        // TODO: Review if we care about unifying which thread message is published on
        //       and which thread it is recieved on. If so need to use IScheduler.

        public DefaultAgentBroker(IChannelSender channelSender, IMessageConverter messageConverter)
        {
            _channelSender = channelSender;
            _messageConverter = messageConverter;
            _subject = new BehaviorSubject<IMessageEnvelope>(null);
            _queue = new BlockingCollection<IMessageEnvelope>();
            
            new Thread(ReadMessages) { IsBackground = true }.Start();

            // TODO: This probably shouldn't be here but don't want to setup 
            //       more infrasture atm. Deciding whether it should be users 
            //       code which instantiates listners or if we provider infrasturcture
            //       which starts them up and triggers the subscription. 
            //ListenAll().Subscribe(async msg => await channelSender.PublishMessage(msg));
        }

        private void ReadMessages()
        {
            while (true)
            {
                var message = _queue.Take();

                // run through all listeners
                 _subject.OnNext(message);

                // TODO: Implement isCancelled at this point
                ////if (!message.IsCancelled)
                _channelSender.PublishMessage(message);
            }
        }

        public IObservable<T> Listen<T>() 
        {
            return ListenIncludeLatest<T>().Skip(1);
        }

        public IObservable<T> ListenIncludeLatest<T>() 
        {
            return _subject
                .Where(msg => typeof(T).GetTypeInfo().IsAssignableFrom(msg.Payload.GetType().GetTypeInfo()))
                .Select(msg => (T)msg);
        }
        public IObservable<IMessageEnvelope> ListenAll()
        {
            return ListenAllIncludeLatest().Skip(1);
        }

        public IObservable<IMessageEnvelope> ListenAllIncludeLatest()
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