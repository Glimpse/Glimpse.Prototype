using Glimpse.Internal;

namespace Glimpse.Agent.Messages
{
    public class AfterActionResultMessage
    {
        public string ActionId { get; set; }

        public Timing Timing { get; set; }
    }
}