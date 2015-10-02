using System;
using System.Collections.Generic;
using Glimpse.Agent.Web.Broker;
using Microsoft.AspNet.Http;

namespace Glimpse.Agent.Web.Messages
{
    public class EndRequestMessage
    {
        [PromoteToDuration]
        public double? Duration { get; set; }

        [PromoteToDateTime]
        public DateTime? StartTime { get; set;  }

        public DateTime EndTime { get; set; }

        [PromoteToUrl]
        public string Url { get; set; }

        public string Path { get; set; }

        public string QueryString { get; set; }

        public long? ContentLength { get; set; }

        [PromoteToContentType]
        public string ContentType { get; set; }

        [PromoteToStatusCode]
        public int StatusCode { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Headers { get; set; }
    }
}