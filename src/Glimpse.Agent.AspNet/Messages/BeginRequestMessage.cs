using System.Collections.Generic;
using Glimpse.Internal;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Primitives;

namespace Glimpse.Agent.Messages
{
    public class BeginRequestMessage
    {
        [PromoteToUrl]
        public string Url { get; set; }

        public string Path { get; set; }

        public string QueryString { get; set; }

        [PromoteToMethod]
        public string Method { get; set; }

        public IDictionary<string, StringValues> Headers { get; set; }

        public long? ContentLength { get; set; }

        public string ContentType { get; set; }
    }
}