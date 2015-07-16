
namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public class ActionInvokedMessage
    {
        public string ActionId { get; set; }

        public string DisplayName { get; set; }

        public Timing Timing { get; set; }
    }
}