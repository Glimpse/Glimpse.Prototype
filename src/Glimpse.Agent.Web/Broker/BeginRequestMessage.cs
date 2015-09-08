using Microsoft.AspNet.Http;
using Glimpse.Common.Broker;

namespace Glimpse.Agent.Web
{
    public class BeginRequestMessage
    {
        public BeginRequestMessage(HttpRequest request)
        {
            // TODO: check if there is a better way of doing this
            // TODO: should there be a StartTime property here?
            Url = $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}{request.QueryString}";
        }

        [PromoteToUrl]
        public string Url { get; }
    }
}