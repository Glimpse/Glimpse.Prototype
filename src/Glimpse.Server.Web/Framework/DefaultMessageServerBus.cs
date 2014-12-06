using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    public class DefaultMessageServerBus : IMessageServerBus
    {
        private readonly ISubject<IMessageEnvelope> _subject;
        private readonly IMessageClientPublisher _currentMessagePublisher;

        // TODO: Review if we care about unifying which thread message is published on
        //       and which thread it is recieved on. If so need to use IScheduler.

        // TODO: Review how we think people will want to filter on these messages given 
        //       the lack of structure 

        public DefaultMessageServerBus(IMessageClientPublisher currentMessagePublisher)
        {
            _subject = new BehaviorSubject<IMessageEnvelope>(null);

            // TODO: This probably shouldn't be here but don't want to setup more infrasture atm
            ListenAll().Subscribe(async msg => await currentMessagePublisher.PublishMessage(msg));
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

        public async Task SendMessage(IMessageEnvelope message)
        {
            await Task.Run(() => _subject.OnNext(message));
        }
    }
}