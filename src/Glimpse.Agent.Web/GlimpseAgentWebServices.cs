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
        public static IEnumerable<IServiceDescriptor> GetDefaultServices()
        {
            return GetDefaultServices(null);
        }

        public static IEnumerable<IServiceDescriptor> GetDefaultServices(IConfiguration configuration)
        {
            var describe = new ServiceDescriber(configuration);

            //
            // Options
            //
            yield return describe.Transient<IConfigureOptions<GlimpseAgentWebOptions>, GlimpseAgentWebOptionsSetup>();
            yield return describe.Singleton<IIgnoredUrisProvider, DefaultIgnoredUrisProvider>();
        }
    }
}