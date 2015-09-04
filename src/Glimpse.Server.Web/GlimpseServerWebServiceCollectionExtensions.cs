using System;
using Glimpse.Server.Web;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;

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

            services.TryAdd(GlimpseWebServices.GetDefaultServices());
            services.TryAdd(GlimpseServerServices.GetDefaultServices());
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