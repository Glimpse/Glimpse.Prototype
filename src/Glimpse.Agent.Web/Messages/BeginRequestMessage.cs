using Microsoft.AspNet.Http;
using Glimpse.Common.Broker;

namespace Glimpse.Agent.Web.Messages
{
    public class BeginRequestMessage
    {
        [PromoteToUrl]
        public string Url { get; set; }
    }
}