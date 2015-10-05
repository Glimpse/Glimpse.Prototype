using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Web.Resources
{
    public class BrowserAgentResource : IResource
    {
        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            var response = context.Response;
            response.Headers[HeaderNames.ContentType] = "text/javascript";

            await response.WriteAsync(@"console.log('Hello from browser agent.');");
        }

        public string Name => "BrowserAgentScript";
        public ResourceParameters Parameters => new ResourceParameters(+ResourceParameter.Hash);
        public ResourceType Type => ResourceType.Agent;
    }
}
