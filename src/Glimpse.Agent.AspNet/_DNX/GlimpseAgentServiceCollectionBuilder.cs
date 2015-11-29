#if DNX
using Microsoft.Extensions.DependencyInjection;

namespace Glimpse
{
    public class GlimpseAgentServiceCollectionBuilder : GlimpseServiceCollectionBuilder
    {
        public GlimpseAgentServiceCollectionBuilder(IServiceCollection innerCollection)
            : base(innerCollection)
        {
        }
    }
}
#endif