using System;
using System.Collections.Generic;
using Microsoft.AspNet.Http;

namespace Glimpse.Agent.Web
{
    public class EndRequestMessage : IMessageIndices
    {
        public EndRequestMessage(HttpRequest request, Timing timing)
        {
            Url = $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}{request.QueryString}";
            StartTime = timing.Start;
            EndTime = timing.End;
            Duration = timing.Elapsed.TotalMilliseconds;
            Method = request.Method;

            Indices = new Dictionary<string, object> {
                { "request.duration", Duration },
                { "request.method", Method },
                { "request.url", Url }
            };
        }

        public IReadOnlyDictionary<string, object> Indices { get; set; }

        public double Duration { get; }

        public DateTime StartTime { get; }

        public DateTime EndTime { get; }

        public string Url { get; }

        public string Method { get; }
    }
}