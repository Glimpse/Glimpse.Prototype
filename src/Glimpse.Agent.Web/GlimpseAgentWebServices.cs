using Glimpse.Agent;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using Glimpse.Agent.Web.Options;

namespace Glimpse
{
    public class GlimpseAgentWebServices
    {
        public static IEnumerable<IServiceDescriptor> GetDefaultServices()
        {
            return GetDefaultServices(new Configuration());
        }

        public static IEnumerable<IServiceDescriptor> GetDefaultServices(IConfiguration configuration)
        {
            var describe = new ServiceDescriber(configuration);

            //
            // Options
            //
            yield return describe.Singleton<IIgnoredUrisProvider, DefaultIgnoredUrisProvider>();
        }
    }
}