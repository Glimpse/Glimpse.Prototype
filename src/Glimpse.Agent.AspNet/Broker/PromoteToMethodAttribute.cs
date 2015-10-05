namespace Glimpse.Agent.AspNet.Broker
{
    public class PromoteToMethodAttribute : PromoteToAttribute
    {
        public PromoteToMethodAttribute() : base("request-method")
        {
        }
    }
}
