using Microsoft.Framework.DependencyInjection.Extensions;

namespace Glimpse
{
    public static class GlimpseServerHttpServiceCollectionExtensions
    {
        public static GlimpseServerServiceCollectionBuilder WithRemoteHttpAgent(this GlimpseServerServiceCollectionBuilder services)
        {
            services.TryAdd(GlimpseServerHttpServices.GetDefaultServices());

            return services;
        }
    }
}