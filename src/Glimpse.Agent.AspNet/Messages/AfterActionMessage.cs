
using Glimpse.Internal;

namespace Glimpse.Agent.Messages
{
    public class AfterActionMessage
    {
        public string ActionId { get; set; }

        public Timing Timing { get; set; }
    }
}