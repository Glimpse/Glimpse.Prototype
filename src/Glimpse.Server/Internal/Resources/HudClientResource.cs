using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Server.Resources;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Internal.Resources
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
clientTemplate = script.dataset.clientTemplate;
// TODO: Remove .replace() as 'poor mans' uri template resolution.
link.setAttribute('href', clientTemplate.replace('{&requestId}', '&requestId='+requestId));
link.setAttribute('target', '_blank');
link.text = 'Open Glimpse';
document.body.appendChild(link);");
        }

        public string Name => "HudClientScript";
        public IEnumerable<ResourceParameter> Parameters => new []{ +ResourceParameter.Hash };
        public ResourceType Type => ResourceType.Client;
    }
}
