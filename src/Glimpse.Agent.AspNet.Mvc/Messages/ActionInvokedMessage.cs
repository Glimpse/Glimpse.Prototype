
namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public class ActionInvokedMessage
    {
        public string ActionId { get; set; }

        public string DisplayName { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public Timing Timing { get; set; }
    }
}