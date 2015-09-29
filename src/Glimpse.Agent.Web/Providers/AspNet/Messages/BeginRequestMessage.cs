using Glimpse.Agent.Web.Broker;

namespace Glimpse.Agent.Web.Messages
{
    public class BeginRequestMessage
    {
        [PromoteToUrl]
        public string Url { get; set; }

        [PromoteToMethod]
        public string Method { get; set; }
    }
}