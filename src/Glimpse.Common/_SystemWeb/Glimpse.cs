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
        public static IServiceCollection Start()
        {
            return Start(null);
        }

        public static IServiceCollection Start(IServiceCollection serviceProvider)
        {
            return null;
        }
    }
}
#endif