using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;

namespace Glimpse.Agent
{ 
    public class DefaultMessageAgentBus : IMessageAgentBus
    {
        private readonly ISubject<IMessage> _subject;

        // TODO: Review if we care about unifying which thread message is published on
        //       and which thread it is recieved on. If so need to use IScheduler.

        public DefaultMessageAgentBus(IMessagePublisher currentMessagePublisher)
        {
            _subject = new BehaviorSubject<IMessage>(null);

            // TODO: This probably shouldn't be here but don't want to setup more infrasture atm
            ListenAll().Subscribe(msg => currentMessagePublisher.PublishMessage(msg));
        }

        public IObservable<T> Listen<T>()
            where T : IMessage
        {
            return ListenIncludeLatest<T>().Skip(1);
        }

        public IObservable<T> ListenIncludeLatest<T>()
            where T : IMessage
        {
            return _subject
                .Where(msg => typeof(T).GetTypeInfo().IsAssignableFrom(msg.GetType().GetTypeInfo()))
                .Select(msg => (T)msg);
        }
        public IObservable<IMessage> ListenAll()
        {
            return ListenAllIncludeLatest().Skip(1);
        }

        public IObservable<IMessage> ListenAllIncludeLatest()
        {
            return _subject;
        }

        public void SendMessage(IMessage message)
        {
            _subject.OnNext(message);
        }
    }
}