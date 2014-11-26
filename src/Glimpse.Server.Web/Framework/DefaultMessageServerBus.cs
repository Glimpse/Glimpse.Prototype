using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;

namespace Glimpse.Server
{
    public class DefaultMessageServerBus : IMessageServerBus
    {
        private readonly ISubject<IMessageEnvelope> _subject;

        // TODO: Review if we care about unifying which thread message is published on
        //       and which thread it is recieved on. If so need to use IScheduler.

        // TODO: Review how we think people will want to filter on these messages given 
        //       the lack of structure 

        public DefaultMessageServerBus()
        {
            _subject = new BehaviorSubject<IMessageEnvelope>(null);
        }

        public IObservable<T> Listen<T>()
            where T : IMessageEnvelope
        {
            return ListenIncludeLatest<T>().Skip(1);
        }

        public IObservable<T> ListenIncludeLatest<T>()
            where T : IMessageEnvelope
        {
            return _subject
                .Where(msg => typeof(T).GetTypeInfo().IsAssignableFrom(msg.GetType().GetTypeInfo()))
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

        public void SendMessage(IMessageEnvelope message)
        {
            _subject.OnNext(message);
        }
    }
}