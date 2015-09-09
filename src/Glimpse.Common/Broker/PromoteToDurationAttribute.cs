namespace Glimpse.Common.Broker
{
    public class PromoteToDurationAttribute : PromoteToAttribute
    {
        public PromoteToDurationAttribute() : base("request-duration")
        {
        }
    }
}
