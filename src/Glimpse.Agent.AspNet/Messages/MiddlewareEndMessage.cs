using System;

namespace Glimpse.Agent.Messages
{
    public class MiddlewareEndMessage
    {
        public Guid CorrelationId { get; set; }

        public double? Duration { get; set; }
    }
}
