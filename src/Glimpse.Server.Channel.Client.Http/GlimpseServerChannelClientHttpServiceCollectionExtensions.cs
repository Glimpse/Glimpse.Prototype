using Microsoft.Framework.DependencyInjection.Extensions;

namespace Glimpse
{
    public static class GlimpseServerChannelClientHttpServiceCollectionExtensions
    {
        public static GlimpseServerServiceCollectionBuilder WithRemoteHttpClient(this GlimpseServerServiceCollectionBuilder services)
        {
            services.TryAdd(GlimpseServerChannelClientHttpServices.GetDefaultServices());

            return services;
        }
    }
}