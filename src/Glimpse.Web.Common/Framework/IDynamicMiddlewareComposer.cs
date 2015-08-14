using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;

namespace Glimpse.Web
{
    public interface IDynamicMiddlewareComposer
    {
        void Register(IApplicationBuilder applicationBuilder);
    }
}
