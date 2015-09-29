using System;
using Glimpse.Common.Broker;

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

        [PromoteToContentType]
        public string ContentType { get; set; }

        [PromoteToStatusCode]
        public int StatusCode { get; set; }
    }
}