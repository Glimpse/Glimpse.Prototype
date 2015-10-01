namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public interface IActionMessage
    {
        string ActionId { get; set; }

        string DisplayName { get; set; }

        string ActionName { get; set; }

        string ControllerName { get; set; }

        Timing Timing { get; set; }
    }
}
