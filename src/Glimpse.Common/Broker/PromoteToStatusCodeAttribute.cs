namespace Glimpse.Common.Broker
{
    public class PromoteToStatusCodeAttribute : PromoteToAttribute
    {
        public PromoteToStatusCodeAttribute() : base("request-status-code")
        {
        }
    }
}