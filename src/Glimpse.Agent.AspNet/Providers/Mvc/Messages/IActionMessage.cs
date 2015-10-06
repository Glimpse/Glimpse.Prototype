using System;
using Glimpse.Internal;

namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public interface IActionMessage
    {
        string ActionId { get; set; }

        string ActionDisplayName { get; set; }

        string ActionName { get; set; }

        string ActionControllerName { get; set; }

        string ActionTargetClass { get; set; }

        string ActionTargetMethod { get; set; }

        DateTime? ActionStartTime { get; set; }

        DateTime? ActionEndTime { get; set; }

        TimeSpan ActionDuration { get; set; }
    }
}
