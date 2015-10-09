using System;

namespace Glimpse.Agent.Messages
{
    public class AfterActionViewMessage
    {
        public string ActionId { get; set; }

        public string ActionName { get; set; }

        public string ActionControllerName { get; set; }

        public DateTime? ViewEndTime { get; set; }

        public TimeSpan ViewDuration { get; set; }
    }
}
