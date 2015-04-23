using System;
using System.Collections.Generic;
using Glimpse.Web; 
using Microsoft.Framework.DependencyInjection;

namespace Glimpse
{
    public class GlimpseWebServices
    {
        public static IServiceCollection GetDefaultServices()
        {
            var services = new ServiceCollection();
             
            services.AddTransient<IRequestAuthorizerProvider, DefaultRequestAuthorizerProvider>();
            services.AddTransient<IRequestHandlerProvider, DefaultRequestHandlerProvider>();
            services.AddTransient<IRequestRuntimeProvider, DefaultRequestRuntimeProvider>();

            return services;
        }
    }
}