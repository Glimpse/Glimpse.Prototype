namespace Glimpse.Agent.AspNet.Broker
{
    public class PromoteToDurationAttribute : PromoteToAttribute
    {
        public PromoteToDurationAttribute() : base("request-duration")
        {
        }
    }
}
