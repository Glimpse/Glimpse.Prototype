using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace Glimpse.Agent.Messages
{
    public class WebResponseMessage
    {
        [PromoteToUrl]
        public string Url { get; set; }

        [PromoteToStatusCode]
        public int StatusCode { get; set; }

        public IReadOnlyDictionary<string, StringValues> Headers { get; set; }

        public ResponseBody Body { get; set; }

        [PromoteToDuration]
        public double? Duration { get; set; }

        public DateTime EndTime { get; set; }
    }

    public class ResponseBody
    {
        public int Size { get; set; }

        public string Content { get; set; }

        public string Encoding { get; set; }

        public bool IsTruncated { get; set; }
    }
}