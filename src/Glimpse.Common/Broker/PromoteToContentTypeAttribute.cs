namespace Glimpse.Common.Broker
{
    public class PromoteToContentTypeAttribute : PromoteToAttribute
    {
        public PromoteToContentTypeAttribute() : base("request-content-type")
        {
        }
    }
}
