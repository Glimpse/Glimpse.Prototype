namespace Glimpse.Agent.Web.Broker
{
    public class PromoteToStatusCodeAttribute : PromoteToAttribute
    {
        public PromoteToStatusCodeAttribute() : base("request-status-code")
        {
        }
    }
}