using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace Glimpse.Agent.Messages
{
    public class WebRequestMessage
    {
        public RequestProtocol Protocol { get; set; }

        [PromoteToUrl]
        public string Url { get; set; }

        [PromoteToMethod]
        public string Method { get; set; }

        public IReadOnlyDictionary<string, StringValues> Headers { get; set; }
        
        public RequestBody Body { get; set; }

        [PromoteToDateTime]
        public DateTime StartTime { get; set; }

        public bool IsAjax { get; set; }
    }

    public class RequestProtocol
    {
        public string Identifier { get; set; }

        public string Version { get; set; }
    }

    public class RequestBody
    {
        public int Size { get; set; }

        public object Form { get; set; }

        public string Content { get; set; }

        public string Encoding { get; set; }

        public bool IsTruncated { get; set; }
    }
}