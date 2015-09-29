namespace Glimpse.Agent.Web.Broker
{
    public class PromoteToDateTimeAttribute : PromoteToAttribute
    {
        public PromoteToDateTimeAttribute() : base("request-datetime")
        {
        }
    }
}
