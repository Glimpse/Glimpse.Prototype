using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Glimpse.Server.Web
{
    public class DefaultServerBroker : IServerBroker
    {
        //private readonly ISubject<MessageListenerPayload> _subject;
        private readonly IClientBroker _currentMessagePublisher;
        private readonly IStorage _storage;

        // TODO: Review if we care about unifying which thread message is published on
        //       and which thread it is recieved on. If so need to use IScheduler.

        // TODO: Review how we think people will want to filter on these messages given 
        //       the lack of structure 

        public DefaultServerBroker(IClientBroker currentMessagePublisher, IStorage storage)
        {
            // TODO: Should add back in at some point?
            //_subject = new BehaviorSubject<MessageListenerPayload>(null);
            _currentMessagePublisher = currentMessagePublisher;
            _storage = storage;

            //// TODO: This probably shouldn't be here but don't want to setup more infrasture atm
            //ListenAll().Subscribe(async msg => {
            //    await currentMessagePublisher.PublishMessage(msg);
            //    await storage.Persist(msg);
            //});
        }
        
        //public IObservable<MessageListenerPayload> ListenAll()
        //{
        //    return ListenAllIncludeLatest().Skip(1);
        //}

        //public IObservable<MessageListenerPayload> ListenAllIncludeLatest()
        //{
        //    return _subject;
        //}

        public void SendMessage(IMessage message)
        {
            //var notificationOptions = new MessageListenerPayload(message);

            //_subject.OnNext(notificationOptions);

            //if (!notificationOptions.IsCancelled)
            //{
                _currentMessagePublisher.PublishMessage(message);
                _storage.Persist(message);
            //}
        }
    }
}