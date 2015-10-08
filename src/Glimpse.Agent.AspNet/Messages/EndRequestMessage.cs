using System;
using System.Collections.Generic;
using Glimpse.Agent.Internal.Messaging;
using Glimpse.Internal;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Primitives;

namespace Glimpse.Agent.Messages
{
    public class EndRequestMessage
    {
        [PromoteToDuration]
        public double? ResponseDuration { get; set; }

        [PromoteToDateTime]
        public DateTime? RequestStartTime { get; set;  }

        public DateTime ResponseEndTime { get; set; }

        [PromoteToUrl]
        public string RequestUrl { get; set; }

        public string RequestPath { get; set; }

        public string RequestQueryString { get; set; }

        public long? ResponseContentLength { get; set; }

        [PromoteToContentType]
        public string ResponseContentType { get; set; }

        [PromoteToStatusCode]
        public int ResponseStatusCode { get; set; }

        public IDictionary<string, StringValues> ResponseHeaders { get; set; }
    }
}