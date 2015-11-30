#if DNX
using Microsoft.Extensions.DependencyInjection;

namespace Glimpse
{
    public class GlimpseServerServiceCollectionBuilder : GlimpseServiceCollectionBuilder
    {
        public GlimpseServerServiceCollectionBuilder(IServiceCollection innerCollection)
            : base(innerCollection)
        {
        }
    }
}
#endif