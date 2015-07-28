using System;
using System.Collections.Generic;
using Glimpse;
using Glimpse.Web;

namespace Glimpse.Agent.Web
{
    public class BeginRequestMessage : IMessageIndices
    {
        public BeginRequestMessage(IHttpRequest request)
        {
            Url = request.Uri();

            Indices = new Dictionary<string, object> { { "request.url", Url } };
        }

        public IReadOnlyDictionary<string, object> Indices { get; set; }

        public string Url { get; }
    }
}