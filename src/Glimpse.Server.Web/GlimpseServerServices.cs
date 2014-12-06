using Glimpse.Agent;
using Glimpse.Server;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System.Collections.Generic;

namespace Glimpse
{
    public class GlimpseServerServices
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
            yield return describe.Singleton<IMessageServerBus, DefaultMessageServerBus>();
            yield return describe.Singleton<IMessageClientPublisher, DefaultMessageClientPublisher>();
        }

        public static IEnumerable<IServiceDescriptor> GetPublisherServices()
        {
            return GetPublisherServices(new Configuration());
        }

        public static IEnumerable<IServiceDescriptor> GetPublisherServices(IConfiguration configuration)
        {
            var describe = new ServiceDescriber(configuration);

            //
            // Broker
            //
            yield return describe.Singleton<IMessagePublisher, LocalMessagePublisher>();
        }
    }
}