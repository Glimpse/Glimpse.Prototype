using Glimpse.Common.Initialization;
using Glimpse.Initialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Glimpse
{
    public static class GlimpseServiceCollectionExtensions
    {
        public static GlimpseServiceCollectionBuilder AddGlimpse(this IServiceCollection services)
        {
            services.TryAdd(GlimpseServices.GetDefaultServices());

            var extensionProvider = services.BuildServiceProvider().GetService<IExtensionProvider<IRegisterServices>>();

            var glimpseServiceCollectionBuilder = new GlimpseServiceCollectionBuilder(services);

            foreach (var registration in extensionProvider.Instances)
            {
                registration.RegisterServices(glimpseServiceCollectionBuilder);
            }

            return glimpseServiceCollectionBuilder;
        } 
    }
}