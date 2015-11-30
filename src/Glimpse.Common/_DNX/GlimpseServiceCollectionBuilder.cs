#if DNX
using Glimpse.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Glimpse
{
    public class GlimpseServiceCollectionBuilder : ServiceCollectionWrapper
    {
        public GlimpseServiceCollectionBuilder(IServiceCollection innerCollection) 
            : base(innerCollection)
        {
        }
    }
}
#endif