using Glimpse.Agent;
using Glimpse.Agent.Connection.Stream.Connection;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;

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