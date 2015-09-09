using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Glimpse.Server.Web;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Agent.Browser.Resources
{
    public class BrowserAgentResource : IResource
    {
        public void Register(IApplicationBuilder appBuilder)
        {
            appBuilder.Map("/browser/agent", childApp => childApp.Run(async context =>
            {
                var response = context.Response;

                response.Headers[HeaderNames.ContentType] = "application/javascript";

                var assembly = typeof(BrowserAgentResource).GetTypeInfo().Assembly;

                var jqueryStream = assembly.GetManifestResourceStream("Glimpse.Agent.Browser.Resources.Embed.scripts.jquery.jquery-2.1.1.js");
                var signalrStream = assembly.GetManifestResourceStream("Glimpse.Agent.Browser.Resources.Embed.scripts.signalr.jquery.signalR-2.2.0.js");
                var agentStream = assembly.GetManifestResourceStream("Glimpse.Agent.Browser.Resources.Embed.scripts.BrowserAgent.js");

                using (var jqueryReader = new StreamReader(jqueryStream, Encoding.UTF8))
                using (var signalrReader = new StreamReader(signalrStream, Encoding.UTF8))
                using (var agentReader = new StreamReader(agentStream, Encoding.UTF8))
                {
                    // TODO: The worlds worst hack!!! Nik did this...
                    await response.WriteAsync(jqueryReader.ReadToEnd() + signalrReader.ReadToEnd() + agentReader.ReadToEnd());
                }
            }));
        }

        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            var response = context.Response;

            response.Headers[HeaderNames.ContentType] = "application/javascript";

            var assembly = typeof(BrowserAgentResource).GetTypeInfo().Assembly;

            var jqueryStream = assembly.GetManifestResourceStream("Glimpse.Agent.Browser.Resources.Embed.scripts.jquery.jquery-2.1.1.js");
            var signalrStream = assembly.GetManifestResourceStream("Glimpse.Agent.Browser.Resources.Embed.scripts.signalr.jquery.signalR-2.2.0.js");
            var agentStream = assembly.GetManifestResourceStream("Glimpse.Agent.Browser.Resources.Embed.scripts.BrowserAgent.js");

            using (var jqueryReader = new StreamReader(jqueryStream, Encoding.UTF8))
            using (var signalrReader = new StreamReader(signalrStream, Encoding.UTF8))
            using (var agentReader = new StreamReader(agentStream, Encoding.UTF8))
            {
                // TODO: The worlds worst hack!!! Nik did this...
                await response.WriteAsync(jqueryReader.ReadToEnd() + signalrReader.ReadToEnd() + agentReader.ReadToEnd());
            }
        }

        public string Name => "BrowserAgent";

        public ResourceParameters Parameters => null;

        public ResourceType Type => ResourceType.Agent;
    }
}