namespace Glimpse.Agent.Web.Broker
{
    public class PromoteToContentTypeAttribute : PromoteToAttribute
    {
        public PromoteToContentTypeAttribute() : base("request-content-type")
        {
        }
    }
}
