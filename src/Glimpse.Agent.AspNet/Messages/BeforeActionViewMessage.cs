using System;

namespace Glimpse.Agent.Messages
{
    public class BeforeActionViewMessage
    {
        public string ActionId { get; set; }

        public string ActionName { get; set; }

        public string ActionControllerName { get; set; }

        public string ViewPath { get; set; }

        public ViewResultData ViewData { get; set; }

        public DateTime? ViewStartTime { get; set; }

    }
}