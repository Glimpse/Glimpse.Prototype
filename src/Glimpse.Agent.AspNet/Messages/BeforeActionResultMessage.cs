namespace Glimpse.Agent.Messages
{
    public class BeforeActionResultMessage
    {
        public string ActionId { get; set; }

        public string ActionDisplayName { get; set; }

        public string ActionName { get; set; }

        public string ActionControllerName { get; set; }

        public ActionResultData ActionResult { get; set; }
    }
}