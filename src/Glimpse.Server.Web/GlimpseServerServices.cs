using Glimpse.Agent;
using Glimpse.Server;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System.Collections.Generic;
using Glimpse.Server.Options;
using Microsoft.Framework.OptionsModel;

namespace Glimpse
{
    public class GlimpseServerServices
    {
        public static IEnumerable<IServiceDescriptor> GetDefaultServices()
        {
            return GetDefaultServices(null);
        }

        public static IEnumerable<IServiceDescriptor> GetDefaultServices(IConfiguration configuration)
        {
            var describe = new ServiceDescriber(configuration);

            //
            // Broker
            //
            yield return describe.Singleton<IServerBroker, DefaultServerBroker>();
            yield return describe.Singleton<IClientBroker, DefaultClientBroker>();

            //
            // Store
            //
            yield return describe.Singleton<IStorage, InMemoryStorage>();

            //
            // Options
            //
            yield return describe.Transient<IConfigureOptions<GlimpseServerWebOptions>, GlimpseServerWebOptionsSetup>();
            yield return describe.Singleton<IAllowRemoteProvider, DefaultAllowRemoteProvider>();
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
            yield return describe.Singleton<IChannelSender, InProcessChannel>();
        }
    }
}