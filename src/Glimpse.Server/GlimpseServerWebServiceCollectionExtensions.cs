using System;
using Glimpse.Server.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Glimpse
{
    public static class GlimpseServerWebServiceCollectionExtensions
    {
        public static GlimpseServerServiceCollectionBuilder RunningServerWeb(this GlimpseServiceCollectionBuilder services)
        {
            return services.RunningServerWeb(null);
        }

        public static GlimpseServerServiceCollectionBuilder RunningServerWeb(this GlimpseServiceCollectionBuilder services, Action<GlimpseServerWebOptions> setupAction)
        {
            services.AddOptions();
            
            services.TryAdd(GlimpseServerWebServices.GetDefaultServices());

            if (setupAction != null)
            {
                services.Configure(setupAction);
            }

            return new GlimpseServerServiceCollectionBuilder(services);
        }

        public static IServiceCollection WithLocalAgent(this GlimpseServerServiceCollectionBuilder services)
        { 
            return services.Add(GlimpseServerWebServices.GetLocalAgentServices());
        }
    }
}