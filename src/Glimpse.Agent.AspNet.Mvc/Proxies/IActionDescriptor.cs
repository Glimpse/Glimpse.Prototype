
namespace Glimpse.Agent.AspNet.Mvc.Proxies
{
    public interface IActionDescriptor
    {
        string Id { get; }
        string DisplayName { get; }
        string Name { get; }
        string ControllerName { get; }
    }
}
