using System;

namespace Glimpse.Agent.Messages
{
    public class MiddlewareStartMessage
    {
        public Guid CorrelationId { get; set; }

        public string Name { get; set; }

        public string PacakgeName { get; set; }

        public string DisplaName { get; set; }
    }
}
