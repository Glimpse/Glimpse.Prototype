using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Initialization;
using Glimpse.Server.Resources;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

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
    ""browserAgentScriptTemplate"" : ""{scriptOptions.BrowserAgentScriptTemplate}"",
    ""hudScriptTemplate"" : ""{scriptOptions.HudScriptTemplate}"",
    ""httpMessageTemplate"" : ""{scriptOptions.HttpMessageTemplate}"",
    ""metadataTemplate"" : ""{scriptOptions.MetadataTemplate}"",
    ""clientScriptTemplate"" : ""{scriptOptions.ClientScriptTemplate}""
}}";

            var response = context.Response;
            response.Headers[HeaderNames.ContentType] = "application/json";
            response.Headers[HeaderNames.ContentDisposition] = "attachment; filename = glimpse.json";
            await response.WriteAsync(json);
        }

        public string Name => "ScriptOptions";
        public IEnumerable<ResourceParameter> Parameters => new [] { ResourceParameter.Hash };
        public ResourceType Type => ResourceType.Client;
    }
}
