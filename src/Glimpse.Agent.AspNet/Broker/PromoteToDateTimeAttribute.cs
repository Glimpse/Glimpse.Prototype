namespace Glimpse.Agent.AspNet.Broker
{
    public class PromoteToDateTimeAttribute : PromoteToAttribute
    {
        public PromoteToDateTimeAttribute() : base("request-datetime")
        {
        }
    }
}
