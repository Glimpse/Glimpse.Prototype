using Glimpse.Agent.Connection.Stream.Connection;
using System;
using System.Threading.Tasks;

namespace Glimpse.Agent
{
    public class WebSocketChannelSender : IChannelSender
    {
        private readonly IMessageConverter _messageConverter;
        private readonly IStreamHubProxyFactory _streamHubProxyFactory;
        private IStreamHubProxy _streamHubProxy;

        public WebSocketChannelSender(IMessageConverter messageConverter, IStreamHubProxyFactory streamHubProxyFactory)
        {
            _messageConverter = messageConverter;
            _streamHubProxyFactory = streamHubProxyFactory;
            _streamHubProxyFactory.Register("WebSocketChannelReceiver", x => _streamHubProxy = x);
        }

        public async Task PublishMessage(object payload)
        {
            // TODO: Probably not the best place to put this
            await _streamHubProxyFactory.Start();

            var message = _messageConverter.ConvertMessage(payload);

            await _streamHubProxy.Invoke("HandleMessage", message);
        }
    }
}