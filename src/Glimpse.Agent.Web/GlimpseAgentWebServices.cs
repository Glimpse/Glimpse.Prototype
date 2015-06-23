using Glimpse.Agent;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using Glimpse.Agent.Web;
using Glimpse.Agent.Web;
using Microsoft.Framework.OptionsModel;

namespace Glimpse
{
    public class GlimpseAgentWebServices
    {
        public static IServiceCollection GetDefaultServices()
        {
            var services = new ServiceCollection();

            //
            // Options
            //
            services.AddTransient<IConfigureOptions<GlimpseAgentWebOptions>, GlimpseAgentWebOptionsSetup>();
            services.AddSingleton<IRequestIgnorerUriProvider, DefaultRequestIgnorerUriProvider>();
            services.AddSingleton<IRequestIgnorerStatusCodeProvider, DefaultRequestIgnorerStatusCodeProvider>();
            services.AddSingleton<IRequestIgnorerContentTypeProvider, DefaultRequestIgnorerContentTypeProvider>();
            services.AddSingleton<IRequestIgnorerProvider, DefaultRequestIgnorerProvider>();
            services.AddSingleton<IRequestProfilerProvider, DefaultRequestProfilerProvider>();

            return services;
        }
    }
}