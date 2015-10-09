using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glimpse.Agent.Internal.Inspectors.Mvc.Proxies
{
    public interface IViewContext
    {
        object ActionDescriptor { get; }
        IHttpContext HttpContext { get; }
        IRouteData RouteData { get; }
        IDictionary<string, object> TempData { get; }
        IDictionary<string, object> ViewData { get; }
    }
}
