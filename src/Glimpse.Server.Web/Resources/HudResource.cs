using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Web.Resources
{
    public class HudResource : IResource
    {
        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            var response = context.Response;
            response.Headers[HeaderNames.ContentType] = "text/javascript";

            await response.WriteAsync(@"
var script = document.querySelector('script[data-request-id]'),
link = document.createElement('a'),
requestId = script.dataset.requestId;
link.setAttribute('href', '/glimpse/client/index.html?id=' + requestId);
link.setAttribute('target', '_blank');
link.text = 'Open Glimpse';
document.body.appendChild(link);");
        }

        public string Name => "HudClientScript";
        public ResourceParameters Parameters => ResourceParameters.None;
        public ResourceType Type => ResourceType.Client;
    }
}
