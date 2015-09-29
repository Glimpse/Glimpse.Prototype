
namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public class BeforeActionMessage : IActionRouteFoundMessage
    {
        public string ActionId { get; set; }

        public string DisplayName { get; set; }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public RouteData RouteData { get; set; }
    }
}