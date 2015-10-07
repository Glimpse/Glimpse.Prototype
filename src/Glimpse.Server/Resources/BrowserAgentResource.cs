using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Server.Web;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Resources
{
    public class BrowserAgentResource : IResource
    {
        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            ////TODO: Read from file system the script
            //var assembly = typeof(BrowserAgentResource).GetTypeInfo().Assembly;
            //var agentStream = assembly.GetManifestResourceStream("Glimpse.Server.Resources.Embed.BrowserAgent.scripts.BrowserAgent.js");

            var response = context.Response;
            response.Headers[HeaderNames.ContentType] = "text/javascript";

            await response.WriteAsync(@"console.log('Hello from browser agent.');");
        }

        public string Name => "BrowserAgentScript";

        public IEnumerable<ResourceParameter> Parameters => new [] { +ResourceParameter.Hash};

        public ResourceType Type => ResourceType.Agent;
    }
}
