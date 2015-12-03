#if SystemWeb
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Glimpse
{
    public static class Glimpse
    {
        public static GlimpseServiceCollectionBuilder Start()
        {
            return Start(new ServiceCollection());
        }

        public static GlimpseServiceCollectionBuilder Start(IServiceCollection serviceProvider)
        {
            return serviceProvider.AddGlimpse();
        }
    }
}
#endif