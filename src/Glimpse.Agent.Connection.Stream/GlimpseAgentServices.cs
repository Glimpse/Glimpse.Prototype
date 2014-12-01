using Glimpse.Agent;
using Glimpse.Agent.Connection.Stream.Connection;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Glimpse
{
    public class GlimpseAgentServices
    {
        public static IEnumerable<IServiceDescriptor> GetDefaultServices()
        {
            return GetDefaultServices(new Configuration());
        }

        public static IEnumerable<IServiceDescriptor> GetDefaultServices(IConfiguration configuration)
        {
            var describe = new ServiceDescriber(configuration);

            //
            // Broker
            //
            yield return describe.Singleton<IMessagePublisher, RemoteStreamMessagePublisher>();

            //
            // Connection
            //
            yield return describe.Singleton<IStreamProxy, DefaultStreamProxy>();
        }
    }
}