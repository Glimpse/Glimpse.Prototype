using Glimpse.Agent;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse
{
    public class GlimpseAgentHttpServices
    {
        public static IServiceCollection GetDefaultServices()
        { 
            var services = new ServiceCollection();

            //
            // Broker
            //
            services.AddSingleton<IMessagePublisher, HttpMessagePublisher>();


            return services;
        }
    }
}