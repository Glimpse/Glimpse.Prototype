using Glimpse.Agent;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Glimpse
{
    public class GlimpseAgentServices
    {
        public static IServiceCollection GetDefaultServices()
        {
            var services = new ServiceCollection();

            //
            // Broker
            //
            services.AddSingleton<IAgentBroker, DefaultAgentBroker>();
            services.AddTransient<IMessagePublisher, HttpMessagePublisher>();

            return services;
        }
    }
}