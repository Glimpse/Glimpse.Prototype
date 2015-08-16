using Glimpse.Agent.Web;
using Microsoft.Framework.DependencyInjection;
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
            services.AddSingleton<IMiddlewareProfilerComposerProvider, DefaultMiddlewareProfilerComposerProvider>();

            return services;
        }
    }
}