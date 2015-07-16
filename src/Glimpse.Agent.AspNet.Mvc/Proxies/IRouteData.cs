
using System.Collections.Generic;

namespace Glimpse.Agent.AspNet.Mvc.Proxies
{
    public interface IRouteData
    {
        IReadOnlyList<object> Routers { get; }
        IDictionary<string, object> DataTokens { get; }
        IDictionary<string, object> Values { get; }
    }
}
