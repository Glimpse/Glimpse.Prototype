namespace Glimpse.Agent.AspNet.Broker
{
    public class PromoteToContentTypeAttribute : PromoteToAttribute
    {
        public PromoteToContentTypeAttribute() : base("request-content-type")
        {
        }
    }
}
