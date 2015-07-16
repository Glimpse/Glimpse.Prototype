using Glimpse.Agent.Connection.Stream.Connection;
using System;
using System.Threading.Tasks;

namespace Glimpse.Agent
{
    public class WebSocketChannelSender : IChannelSender
    {
        private readonly IStreamHubProxyFactory _streamHubProxyFactory;
        private IStreamHubProxy _streamHubProxy;

        public WebSocketChannelSender(IStreamHubProxyFactory streamHubProxyFactory)
        {
            _streamHubProxyFactory = streamHubProxyFactory;
            _streamHubProxyFactory.Register("WebSocketChannelReceiver", x => _streamHubProxy = x);
        }

        public async Task PublishMessage(IMessageEnvelope message)
        {
            // TODO: Probably not the best place to put this
            await _streamHubProxyFactory.Start();
            
            await _streamHubProxy.Invoke("HandleMessage", message);
        }
    }
}