using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace Glimpse
{
    public static class GlimpseServerWebServiceCollectionExtensions
    {
        public static GlimpseServerServiceCollectionBuilder RunningServerWeb(this GlimpseServiceCollectionBuilder services)
        {   
            ConfigureDefaultServices(services);

            services.TryAdd(GlimpseWebServices.GetDefaultServices());
            services.TryAdd(GlimpseServerServices.GetDefaultServices());
            services.TryAdd(GlimpseServerWebServices.GetDefaultServices()); 

            return new GlimpseServerServiceCollectionBuilder(services);
        }

        public static IServiceCollection WithLocalAgent(this GlimpseServerServiceCollectionBuilder services)
        { 
            return services.Add(GlimpseServerWebServices.GetLocalAgentServices());
        }
        
        private static void ConfigureDefaultServices(IServiceCollection services)
        {
            services.AddOptions();
        }
    }
}