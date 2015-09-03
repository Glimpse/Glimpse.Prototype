using System;
using System.Collections.Concurrent;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Glimpse.Agent
{
    public class DefaultAgentBroker : IAgentBroker
    {
        private readonly IMessagePublisher _messagePublisher;
        private readonly ISubject<MessageListenerPayload> _onSenderThreadSubject;
        private readonly ISubject<MessageListenerPayload> _offSenderThreadSubject;
        private readonly ISubject<MessageListenerPayload> _offSenderThreadInternalSubject;
        private readonly IContextData<MessageContext> _context; 

        public DefaultAgentBroker(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
            _context = new ContextData<MessageContext>();

            _onSenderThreadSubject = new Subject<MessageListenerPayload>();
            _offSenderThreadSubject = new Subject<MessageListenerPayload>();
            _offSenderThreadInternalSubject = new Subject<MessageListenerPayload>();

            OnSenderThread = new AgentBrokerOptions(_onSenderThreadSubject);
            OffSenderThread = new AgentBrokerOptions(_offSenderThreadInternalSubject);

            // ensure off-request data is observed onto a different thread
            _offSenderThreadInternalSubject.Subscribe(payload => Observable.Start(() => _offSenderThreadSubject.OnNext(payload), TaskPoolScheduler.Default));
        }

        /// <summary>
        /// On the sender thread and is blocking
        /// </summary>
        public AgentBrokerOptions OnSenderThread { get; }

        /// <summary>
        /// Off the sender thread and is not blocking
        /// </summary>
        public AgentBrokerOptions OffSenderThread { get; }
        
        public void SendMessage(object payload)
        {
            // need to fetch context data here as we are about to start switching threads
            var options = new MessageListenerPayload(payload, _context.Value);

            // should be non-blocking but up to implementation 
            _messagePublisher.PublishMessage(options);

            // non-blocking
            _offSenderThreadInternalSubject.OnNext(options);

            // blocking
            _onSenderThreadSubject.OnNext(options);
        }
    }
}
