namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public class BeforeActionResultMessage
    {
        public string ActionId { get; set; }

        public string DisplayName { get; set; }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public ActionResultData ActionResult { get; set; }
    }
}