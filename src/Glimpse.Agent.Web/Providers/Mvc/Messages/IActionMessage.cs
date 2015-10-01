namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public interface IActionMessage
    {
        string ActionId { get; set; }

        string DisplayName { get; set; }

        string ActionName { get; set; }

        string ControllerName { get; set; }

        string TargetClass { get; set; }

        string TargetMethod { get; set; }

        Timing Timing { get; set; }
    }
}
