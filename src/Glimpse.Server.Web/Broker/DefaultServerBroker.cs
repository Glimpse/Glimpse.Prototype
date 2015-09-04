using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Glimpse.Server.Web
{
    public class DefaultServerBroker : IServerBroker
    {
        private readonly ISubject<IMessage> _offRecieverThreadSubject;
        private readonly ISubject<IMessage> _offRecieverThreadInternalSubject;
        private readonly IClientBroker _currentMessagePublisher;
        private readonly IStorage _storage;
        
        public DefaultServerBroker(IClientBroker currentMessagePublisher, IStorage storage)
        {
            _currentMessagePublisher = currentMessagePublisher;
            _storage = storage;

            _offRecieverThreadSubject = new Subject<IMessage>();
            _offRecieverThreadInternalSubject = new Subject<IMessage>();

            OffRecieverThread = new ServerBrokerHook(_offRecieverThreadSubject);

            // ensure off-request data is observed onto a different thread
            _offRecieverThreadInternalSubject.Subscribe(payload => Observable.Start(() => _offRecieverThreadSubject.OnNext(payload), TaskPoolScheduler.Default));
        }

        /// <summary>
        /// Off the reciever thread and is not blocking
        /// </summary>
        public ServerBrokerHook OffRecieverThread { get; }
        
        public void SendMessage(IMessage message)
        {
            // non-blocking
            _offRecieverThreadInternalSubject.OnNext(message);

            // TODO: This could probably be altered to be a listener of OffRecieverThread
            // blocking
            _currentMessagePublisher.PublishMessage(message);
            _storage.Persist(message); 
        }
    }
}