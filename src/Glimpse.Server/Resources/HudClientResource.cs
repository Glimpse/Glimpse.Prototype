using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Server.Web;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Resources
{
    public class HudClientResource : IResource
    {
        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            var response = context.Response;
            response.Headers[HeaderNames.ContentType] = "text/javascript";

            await response.WriteAsync(@"
var script = document.querySelector('script[data-request-id]'),
link = document.createElement('a'),
requestId = script.dataset.requestId,
clientUrl = script.dataset.clientUrl;
link.setAttribute('href', clientUrl);
link.setAttribute('target', '_blank');
link.text = 'Open Glimpse';
document.body.appendChild(link);");
        }

        public string Name => "HudClientScript";
        public ResourceParameters Parameters => ResourceParameters.None;
        public ResourceType Type => ResourceType.Agent;
    }
}
