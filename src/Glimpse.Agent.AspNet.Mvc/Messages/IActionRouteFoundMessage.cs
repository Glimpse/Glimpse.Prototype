namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public interface IActionRouteFoundMessage
    {
        string ActionId { get; set; }
        
        RouteData RouteData { get; set; }
    }
}