using Microsoft.Framework.DependencyInjection;

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