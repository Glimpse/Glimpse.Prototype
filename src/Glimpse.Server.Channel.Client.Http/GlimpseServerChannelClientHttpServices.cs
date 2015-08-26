using Microsoft.Framework.DependencyInjection;
using Glimpse.Server.Web;

namespace Glimpse
{
    public class GlimpseServerChannelClientHttpServices
    {
        public static IServiceCollection GetDefaultServices()
        {
            var services = new ServiceCollection();

            //
            // Broker
            //
            services.AddSingleton<IClientBroker, HttpClientBroker>();

            return services;
        }
    }
}