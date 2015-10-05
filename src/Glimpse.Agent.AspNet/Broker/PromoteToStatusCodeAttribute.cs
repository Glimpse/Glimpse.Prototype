namespace Glimpse.Agent.AspNet.Broker
{
    public class PromoteToStatusCodeAttribute : PromoteToAttribute
    {
        public PromoteToStatusCodeAttribute() : base("request-status-code")
        {
        }
    }
}