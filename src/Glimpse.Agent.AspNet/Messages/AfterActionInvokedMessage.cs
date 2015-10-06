using System;

namespace Glimpse.Agent.Messages
{
    public class AfterActionInvokedMessage : IActionMessage
    {
        public string ActionId { get; set; }

        public string ActionDisplayName { get; set; }

        public string ActionName { get; set; }

        public string ActionControllerName { get; set; }

        public string ActionTargetClass { get; set; }

        public string ActionTargetMethod { get; set; }
        
        public DateTime? ActionStartTime { get; set; }

        public DateTime? ActionEndTime { get; set; }

        public TimeSpan ActionDuration { get; set; }
    }
}