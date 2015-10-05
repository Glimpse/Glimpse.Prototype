using System;
using System.Collections;
using System.Collections.Generic;
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