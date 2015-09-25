namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public class AfterActionResultMessage
    {
        public string ActionId { get; set; }

        public Timing Timing { get; set; }
    }
}