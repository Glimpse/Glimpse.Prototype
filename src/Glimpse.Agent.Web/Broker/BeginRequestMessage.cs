using Microsoft.AspNet.Http;
using System.Collections.Generic;

namespace Glimpse.Agent.Web
{
    public class BeginRequestMessage : IMessageIndices
    {
        public BeginRequestMessage(HttpRequest request)
        {
            // TODO: check if there is a better way of doing this
            Url = $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}{request.QueryString}";

            Indices = new Dictionary<string, object> { { "request.url", Url } };
        }

        public IReadOnlyDictionary<string, object> Indices { get; set; }

        public string Url { get; }
    }
}