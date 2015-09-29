
namespace Glimpse.Agent.AspNet.Mvc.Proxies
{
    public interface IActionContext
    {
        IActionDescriptor ActionDescriptor { get; }

        IHttpContext HttpContext { get; }

        IRouteData RouteData { get; }
    }
}
