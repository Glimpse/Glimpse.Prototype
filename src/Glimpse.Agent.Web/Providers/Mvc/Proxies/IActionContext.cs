
namespace Glimpse.Agent.AspNet.Mvc.Proxies
{
    public interface IActionContext
    {
        object ActionDescriptor { get; }

        IHttpContext HttpContext { get; }

        IRouteData RouteData { get; }
    }
}
