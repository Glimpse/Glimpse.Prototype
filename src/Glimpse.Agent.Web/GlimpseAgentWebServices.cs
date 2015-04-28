using Glimpse.Agent;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using Glimpse.Agent.Web;
using Glimpse.Agent.Web.Options;
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
            services.AddSingleton<IIgnoredUrisProvider, DefaultIgnoredUrisProvider>();
            services.AddSingleton<IIgnoredStatusCodeProvider, DefaultIgnoredStatusCodeProvider>();
            services.AddSingleton<IIgnoredContentTypeProvider, DefaultIgnoredContentTypeProvider>();
            services.AddSingleton<IIgnoredRequestPolicyProvider, DefaultIgnoredRequestPolicyProvider>();
            services.AddSingleton<IRequestProfilerProvider, DefaultRequestProfilerProvider>();

            return services;
        }
    }
}