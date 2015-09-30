using System.Collections.Generic;
using Glimpse.Agent.Web.Broker;
using Microsoft.AspNet.Http;

namespace Glimpse.Agent.Web.Messages
{
    public class BeginRequestMessage
    {
        [PromoteToUrl]
        public string Url { get; set; }

        [PromoteToMethod]
        public string Method { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Headers { get; set; }

        public long? ContentLength { get; set; }

        public string ContentType { get; set; }
    }
}