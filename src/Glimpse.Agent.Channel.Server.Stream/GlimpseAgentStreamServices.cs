using Glimpse.Agent;
using Glimpse.Agent.Channel.Server.Stream.Connection;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse
{
    public class GlimpseAgentStreamServices
    {
        public static IServiceCollection GetDefaultServices()
        {
            var services = new ServiceCollection();

            //
            // Broker
            //
            //yield return describe.Singleton<IChannelSender, RemoteStreamMessagePublisher>();
            services.AddSingleton<IChannelSender, WebSocketChannelSender>();

            //
            // Connection
            //
            //yield return describe.Singleton<IStreamProxy, DefaultStreamProxy>();
            services.AddSingleton<IStreamHubProxyFactory, SignalrStreamHubProxyFactory>();

            return services;
        }
    }
}