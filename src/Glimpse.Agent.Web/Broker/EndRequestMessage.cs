using System;
using Glimpse.Common.Broker;
using Microsoft.AspNet.Http;

namespace Glimpse.Agent.Web
{
    public class EndRequestMessage
    {
        public EndRequestMessage(HttpRequest request, HttpResponse response, Timing timing)
        {
            // TODO: check if there is a better way of doing this
            Url = $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}{request.QueryString}";
            StartTime = timing.Start;
            EndTime = timing.End;
            Duration = timing.Elapsed.TotalMilliseconds;
            Method = request.Method;
            ContentType = response.ContentType;
            StatusCode = response.StatusCode;
        }

        [PromoteToDuration]
        public double? Duration { get; }

        [PromoteToDateTime]
        public DateTime? StartTime { get; }

        public DateTime EndTime { get; }

        [PromoteToUrl]
        public string Url { get; }

        [PromoteToMethod]
        public string Method { get; }

        [PromoteToContentType]
        public string ContentType { get; }

        [PromoteToStatusCode]
        public int StatusCode { get; }
    }
}