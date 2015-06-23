using Glimpse.Agent;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;

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
            services.AddSingleton<IChannelSender, HttpChannelSender>();


            return services;
        }
    }
}