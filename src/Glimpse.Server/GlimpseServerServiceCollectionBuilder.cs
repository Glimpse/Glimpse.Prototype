using System;
using System.Collections;
using System.Collections.Generic;
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