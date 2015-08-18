using Microsoft.Framework.DependencyInjection.Extensions;

namespace Glimpse
{
    public static class GlimpseServerStreamServiceCollectionExtensions
    {
        public static GlimpseServerServiceCollectionBuilder WithRemoteStreamAgent(this GlimpseServerServiceCollectionBuilder services)
        {
            services.TryAdd(GlimpseServerStreamServices.GetDefaultServices());

            return services;
        }
    }
}