
using Glimpse.Internal;

namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public class AfterActionMessage
    {
        public string ActionId { get; set; }

        public Timing Timing { get; set; }
    }
}