using System;

namespace Glimpse.Agent.Messages
{
    public class MiddlewareStartMessage
    {
        public Guid CorrelationId { get; set; }

        public string Name { get; set; }

        public string PackageName { get; set; }

        public string DisplaName { get; set; }
    }
}
