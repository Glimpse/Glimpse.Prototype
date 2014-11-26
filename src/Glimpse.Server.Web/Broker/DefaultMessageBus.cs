using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;

namespace Glimpse.Server
{
    public class DefaultMessageBus : IMessageBus
    {
        private readonly ISubject<IMessage> _subject;

        // TODO: Review if we care about unifying which thread message is published on
        //       and which thread it is recieved on. If so need to use IScheduler.

        public DefaultMessageBus()
        {
            _subject = new BehaviorSubject<IMessage>(null);
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