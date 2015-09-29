namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public class AfterActionInvokedMessage
    {
        public string ActionId { get; set; }

        public Timing Timing { get; set; }
    }
}