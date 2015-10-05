namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public interface IActionRouteMessage
    {
        string ActionId { get; set; }
        
        RouteData RouteData { get; set; }
    }
}