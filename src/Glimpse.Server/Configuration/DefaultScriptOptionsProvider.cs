using System.Collections.Generic;
using Glimpse.Initialization;
using Glimpse.Internal.Extensions;
using Glimpse.Server.Internal.Extensions;
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

            var browserAgentScriptTemplate = new UriTemplate(resources.GetValueOrDefault("browser-agent-script", string.Empty), true);
            var httpMessageTemplate = new UriTemplate(resources.GetValueOrDefault("agent-message", string.Empty), true);
            var hudScriptTemplate = new UriTemplate(resources.GetValueOrDefault("hud-client-script", string.Empty), true);
            var metadataTemplate = new UriTemplate(resources.GetValueOrDefault("metadata", string.Empty), true);
            var clientScriptTemplate = new UriTemplate(resources.GetValueOrDefault("client", string.Empty), true);

            return new ScriptOptions
            {
                BrowserAgentScriptTemplate = browserAgentScriptTemplate.ResolveWith(supportedParameters),
                HttpMessageTemplate = httpMessageTemplate.ResolveWith(supportedParameters),
                HudScriptTemplate = hudScriptTemplate.ResolveWith(supportedParameters),
                MetadataTemplate = metadataTemplate.ResolveWith(supportedParameters),
                ClientScriptTemplate = clientScriptTemplate.ResolveWith(supportedParameters)
            };
        }
    }
}
