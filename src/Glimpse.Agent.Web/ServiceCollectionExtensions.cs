using System;
using Glimpse.Agent.Web; 

namespace Microsoft.Framework.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureGlimpseAgentWebOptions(this IServiceCollection services, Action<GlimpseAgentWebOptions> setupAction)
        { 
            services.Configure(setupAction);
        }
    }
}