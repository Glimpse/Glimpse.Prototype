using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Framework.DependencyInjection;

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