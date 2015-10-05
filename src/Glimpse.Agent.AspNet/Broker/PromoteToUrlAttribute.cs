using Glimpse.Internal;

namespace Glimpse.Agent.AspNet.Broker
{
    public class PromoteToUrlAttribute : PromoteToAttribute
    {
        public PromoteToUrlAttribute() : base("request-url")
        {
        }
    }
}
