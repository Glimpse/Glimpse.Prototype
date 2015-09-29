namespace Glimpse.Agent.Web.Broker
{
    public class PromoteToDurationAttribute : PromoteToAttribute
    {
        public PromoteToDurationAttribute() : base("request-duration")
        {
        }
    }
}
