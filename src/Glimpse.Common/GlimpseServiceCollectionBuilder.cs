using Microsoft.Extensions.DependencyInjection;
using Glimpse.Internal;

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