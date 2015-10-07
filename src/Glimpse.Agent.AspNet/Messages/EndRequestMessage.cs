using System;
using System.Collections.Generic;
using Glimpse.Internal;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Primitives;

namespace Glimpse.Agent.Messages
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

        public IDictionary<string, StringValues> Headers { get; set; }
    }
}