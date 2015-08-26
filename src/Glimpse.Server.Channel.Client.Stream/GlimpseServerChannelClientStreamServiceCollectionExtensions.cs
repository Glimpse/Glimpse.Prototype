using Microsoft.Framework.DependencyInjection.Extensions;

namespace Glimpse
{
    public static class GlimpseServerChannelClientStreamServiceCollectionExtensions
    {
        public static GlimpseServerServiceCollectionBuilder WithRemoteStreamClient(this GlimpseServerServiceCollectionBuilder services)
        {
            services.TryAdd(GlimpseServerChannelClientStreamServices.GetDefaultServices());

            return services;
        }
    }
}