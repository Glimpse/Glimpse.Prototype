
namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public class ActionSelectedMessage
    {
        public string ActionId { get; set; }

        public string DisplayName { get; set; }

        public RouteData RouteData { get; set; }
    }
}