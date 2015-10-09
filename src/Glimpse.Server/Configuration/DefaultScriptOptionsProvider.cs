using System.Collections.Generic;
using Glimpse.Common;
using Glimpse.Initialization;
using Glimpse.Internal.Extensions;
using Tavis.UriTemplates;

namespace Glimpse.Server.Configuration
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
            var resources = metadata.Resources;
            var supportedParameters = new Dictionary<string, object>{ {"hash", metadata.Hash} };

            var browserAgentScriptTemplate = new UriTemplate(metadata.Resources.GetValueOrDefault("browser-agent-script", string.Empty), resolvePartially:true);
            browserAgentScriptTemplate.AddParameters(supportedParameters);

            var httpMessageTemplate = new UriTemplate(metadata.Resources.GetValueOrDefault("agent-message", string.Empty), resolvePartially: true);
            httpMessageTemplate.AddParameters(supportedParameters);

            var hudClientScriptTemplate = new UriTemplate(metadata.Resources.GetValueOrDefault("hud-client-script", string.Empty), resolvePartially: true);
            hudClientScriptTemplate.AddParameters(supportedParameters);

            var metadataTemplate = new UriTemplate(metadata.Resources.GetValueOrDefault("metadata", string.Empty), resolvePartially: true);
            metadataTemplate.AddParameters(supportedParameters);

            var spaClientScriptTemplate = new UriTemplate(metadata.Resources.GetValueOrDefault("client", string.Empty), resolvePartially: true);
            spaClientScriptTemplate.AddParameters(supportedParameters);

            return new ScriptOptions
            {
                BrowserAgentScriptTemplate = browserAgentScriptTemplate.Resolve(),
                HttpMessageTemplate = httpMessageTemplate.Resolve(),
                HudScriptTemplate = hudClientScriptTemplate.Resolve(),
                MetadataTemplate = metadataTemplate.Resolve(),
                ClientScriptTemplate = spaClientScriptTemplate.Resolve()
            };
        }
    }
}
