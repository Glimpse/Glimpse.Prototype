using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Web;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Glimpse.Server.Web.Resources
{
    public class ScriptOptionsResource : IResource
    {
        private readonly IScriptOptionsProvider _scriptOptionsProvider;
        private readonly JsonSerializer _serializer;

        public ScriptOptionsResource(IScriptOptionsProvider scriptOptionsProvider, JsonSerializer serializer)
        {
            _scriptOptionsProvider = scriptOptionsProvider;
            _serializer = serializer;
        }

        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            var scriptOptions = _scriptOptionsProvider.BuildInstance();
            _serializer.Formatting = Formatting.Indented;
            var result = _serializer.Serialize(scriptOptions);

            var response = context.Response;
            response.Headers[HeaderNames.ContentType] = "application/json";
            response.Headers[HeaderNames.ContentDisposition] = "attachment; filename = glimpse.json";
            await response.WriteAsync(result);
        }

        public string Name => "ScriptOptions";
        public ResourceParameters Parameters => new ResourceParameters(ResourceParameter.Hash);
        public ResourceType Type => ResourceType.Client;
    }
}
