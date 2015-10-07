using System.Collections.Generic;
using System.Linq;
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
clientUrl = script.dataset.clientUrl;
link.setAttribute('href', clientUrl);
link.setAttribute('target', '_blank');
link.text = 'Open Glimpse';
document.body.appendChild(link);");
        }

        public string Name => "HudClientScript";
        public IEnumerable<ResourceParameter> Parameters => Enumerable.Empty<ResourceParameter>();
        public ResourceType Type => ResourceType.Agent;
    }
}
