using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Web;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Web.Resources
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
    ""browserAgentScriptUri"" : ""{scriptOptions.BrowserAgentScriptUri}"",
    ""hudClientScriptUri"" : ""{scriptOptions.HudClientScriptUri}"",
    ""httpMessageUri"" : ""{scriptOptions.HttpMessageUri}"",
    ""metadataUri"" : ""{scriptOptions.MetadataUri}"",
    ""spaClientScriptUri"" : ""{scriptOptions.SpaClientScriptUri}""
}}";

            var response = context.Response;
            response.Headers[HeaderNames.ContentType] = "application/json";
            response.Headers[HeaderNames.ContentDisposition] = "attachment; filename = glimpse.json";
            await response.WriteAsync(json);
        }

        public string Name => "ScriptOptions";
        public ResourceParameters Parameters => new ResourceParameters(ResourceParameter.Hash);
        public ResourceType Type => ResourceType.Client;
    }
}
