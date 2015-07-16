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

        public async void PublishMessage(IMessage message)
        { 
            // TODO: This is wrong, need to fix it
            try
            {
                await _streamHubProxyFactory.Start();
            
                await _streamHubProxy.Invoke("HandleMessage", message);
            }
            catch (Exception e)
            {
                // TODO: Bad thing happened
            }
        }
    }
}