using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Initialization;
using Glimpse.Server.Resources;
using Microsoft.AspNet.Http;

namespace Glimpse.Server.Internal.Resources
{
    public class ScriptOptionsResource : IResource
    {
        private readonly IScriptOptionsProvider _scriptOptionsProvider;

        public ScriptOptionsResource(IScriptOptionsProvider scriptOptionsProvider)
        {
            _scriptOptionsProvider = scriptOptionsProvider;
        }

        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            var scriptOptions = _scriptOptionsProvider.BuildInstance();
            var json = $@"{{
""scriptOptions"":
    {{
        ""browserAgentScriptTemplate"" : ""{scriptOptions.BrowserAgentScriptTemplate}"",
        ""hudScriptTemplate"" : ""{scriptOptions.HudScriptTemplate}"",
        ""messageIngressTemplate"" : ""{scriptOptions.MessageIngressTemplate}"",
        ""metadataTemplate"" : ""{scriptOptions.MetadataTemplate}"",
        ""clientScriptTemplate"" : ""{scriptOptions.ClientScriptTemplate}""
    }}
}}";

            await context.RespondWith(
                new RawJson(json)
                .AsFile("glimpse.json"));
        }

        public string Name => "script-options";
        public IEnumerable<ResourceParameter> Parameters => new [] { ResourceParameter.Hash };
        public ResourceType Type => ResourceType.Client;
    }
}
