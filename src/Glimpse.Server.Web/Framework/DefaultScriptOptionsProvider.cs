using System.Collections.Generic;
using Glimpse.Web;
using Tavis.UriTemplates;

namespace Glimpse.Server.Web
{
    public class DefaultScriptOptionsProvider : IScriptOptionsProvider
    {
        private readonly IMetadataProvider _metadataProvider;

        public DefaultScriptOptionsProvider(IMetadataProvider metadataProvider)
        {
            _metadataProvider = metadataProvider;
        }

        public ScriptOptions BuildInstance()
        {
            var metadata = _metadataProvider.BuildInstance();
            var supportedParameters = new Dictionary<string, object> {{"hash", metadata.Hash}};

            var browserAgentScriptTemplate = new UriTemplate(metadata.Resources.GetValueOrDefault("browser-agent-script") ?? "");
            browserAgentScriptTemplate.AddParameters(supportedParameters);

            var httpMessageTemplate = new UriTemplate(metadata.Resources.GetValueOrDefault("agent-message") ?? "");
            httpMessageTemplate.AddParameters(supportedParameters);

            var hudClientScriptTemplate = new UriTemplate(metadata.Resources.GetValueOrDefault("hud-client-script") ?? "");
            hudClientScriptTemplate.AddParameters(supportedParameters);

            var metadataTemplate = new UriTemplate(metadata.Resources.GetValueOrDefault("metadata") ?? "");
            metadataTemplate.AddParameters(supportedParameters);

            return new ScriptOptions
            {
                BrowserAgentScriptUri = browserAgentScriptTemplate.Resolve(),
                HttpMessageUri = httpMessageTemplate.Resolve(),
                HudClientScriptUri = hudClientScriptTemplate.Resolve(),
                MetadataUri = metadataTemplate.Resolve()
            };
        }
    }
}
