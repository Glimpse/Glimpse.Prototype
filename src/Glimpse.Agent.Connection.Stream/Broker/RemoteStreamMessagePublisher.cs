using Glimpse.Agent.Connection.Stream.Connection;
using System;
using System.Threading.Tasks;

namespace Glimpse.Agent
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IMessageConverter _messageConverter;
        private readonly IStreamHubProxyFactory _streamHubProxyFactory;
        private IStreamHubProxy _streamHubProxy;

        public MessagePublisher(IMessageConverter messageConverter, IStreamHubProxyFactory streamHubProxyFactory)
        {
            _messageConverter = messageConverter;
            _streamHubProxyFactory = streamHubProxyFactory;
            _streamHubProxyFactory.Register("RemoteStreamMessagePublisherResource", x => _streamHubProxy = x);
        }

        public async Task PublishMessage(IMessage message)
        {
            // TODO: Probably not the best place to put this
            await _streamHubProxyFactory.Start();

            var newMessage = _messageConverter.ConvertMessage(message);

            await _streamHubProxy.Invoke("HandleMessage", newMessage);
        }
    }
}