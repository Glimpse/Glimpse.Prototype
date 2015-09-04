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
        private readonly IMessageConverter _messageConverter;
        private readonly IMessagePublisher _messagePublisher;
        private readonly ISubject<MessagePayloadData> _onSenderThreadSubject;
        private readonly ISubject<MessagePayloadData> _offSenderThreadSubject;
        private readonly ISubject<MessagePayloadData> _offSenderThreadInternalSubject;
        private readonly ISubject<MessagePayloadData> _publisherInternalSubject;
        private readonly IContextData<MessageContext> _context; 

        public DefaultAgentBroker(IMessagePublisher messagePublisher, IMessageConverter messageConverter)
        {
            _messagePublisher = messagePublisher;
            _messageConverter = messageConverter;
            _context = new ContextData<MessageContext>();

            _onSenderThreadSubject = new Subject<MessagePayloadData>();
            _offSenderThreadSubject = new Subject<MessagePayloadData>();
            _offSenderThreadInternalSubject = new Subject<MessagePayloadData>();
            _publisherInternalSubject = new Subject<MessagePayloadData>();

            OnSenderThread = new AgentBrokerHook(_onSenderThreadSubject);
            OffSenderThread = new AgentBrokerHook(_offSenderThreadInternalSubject);

            // ensure off-request data is observed onto a different thread
            _offSenderThreadInternalSubject.Subscribe(payload => Observable.Start(() => _offSenderThreadSubject.OnNext(payload), TaskPoolScheduler.Default));
            _publisherInternalSubject.Subscribe(x => Observable.Start(() => PublishMessage(x), TaskPoolScheduler.Default));
        }

        /// <summary>
        /// On the sender thread and is blocking
        /// </summary>
        public AgentBrokerHook OnSenderThread { get; }

        /// <summary>
        /// Off the sender thread and is not blocking
        /// </summary>
        public AgentBrokerHook OffSenderThread { get; }
        
        public void SendMessage(object payload)
        {
            // need to fetch context data here as we are about to start switching threads
            var data = new MessagePayloadData(payload, _context.Value);
            
            // non-blocking
            _publisherInternalSubject.OnNext(data);

            // non-blocking
            _offSenderThreadInternalSubject.OnNext(data);

            // blocking
            _onSenderThreadSubject.OnNext(data);
        }

        private void PublishMessage(MessagePayloadData data)
        {
            var message = _messageConverter.ConvertMessage(data.Payload, data.Context);

            _messagePublisher.PublishMessage(message);

        }
    }
}
