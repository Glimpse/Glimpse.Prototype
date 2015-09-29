
using System.Collections.Generic;

namespace Glimpse.Agent.AspNet.Mvc.Proxies
{
    public interface IRouteData
    {
        IReadOnlyList<IRouter> Routers { get; }
        IDictionary<string, object> DataTokens { get; }
        IDictionary<string, object> Values { get; }
    }

    public interface IRouter
    {
        string Name { get; }
        string RouteTemplate { get; }
        IDictionary<string, object> Values { get; }
        IReadOnlyList<IRouterTemplatePart> ParsedTemplate { get; }
    }

    public interface IRouterTemplatePart
    {
        string Name { get; }
        object DefaultValue { get; }
        bool IsOptional { get; }
    }
}
