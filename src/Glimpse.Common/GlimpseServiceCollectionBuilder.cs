using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Framework.DependencyInjection;

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