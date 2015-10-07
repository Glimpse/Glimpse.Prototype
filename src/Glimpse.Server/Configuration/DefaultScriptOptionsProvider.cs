using System.Collections.Generic;
using Glimpse.Initialization;
using Glimpse.Common.Internal.Extensions;
using Glimpse.Internal;
using Tavis.UriTemplates;

namespace Glimpse.Server.Configuration
{
    public class DefaultScriptOptionsProvider : IScriptOptionsProvider
    {
        private readonly IMetadataProvider _metadataProvider;
        private readonly IContextData<MessageContext> _context;

        public DefaultScriptOptionsProvider(IMetadataProvider metadataProvider, IContextData<MessageContext> context)
        {
            _metadataProvider = metadataProvider;
            _context = context;
        }

        public ScriptOptions BuildInstance()
        {
            var metadata = _metadataProvider.BuildInstance();
            var supportedParameters = new Dictionary<string, object>
            {
                {"hash", metadata.Hash},
                {"requestid", _context.Value.Id}
            };

            var browserAgentScriptTemplate = new UriTemplate(metadata.Resources.GetValueOrDefault("browser-agent-script") ?? "");
            browserAgentScriptTemplate.AddParameters(supportedParameters);

            var httpMessageTemplate = new UriTemplate(metadata.Resources.GetValueOrDefault("agent-message") ?? "");
            httpMessageTemplate.AddParameters(supportedParameters);

            var hudClientScriptTemplate = new UriTemplate(metadata.Resources.GetValueOrDefault("hud-client-script") ?? "");
            hudClientScriptTemplate.AddParameters(supportedParameters);

            var metadataTemplate = new UriTemplate(metadata.Resources.GetValueOrDefault("metadata") ?? "");
            metadataTemplate.AddParameters(supportedParameters);

            var spaClientScriptTemplate = new UriTemplate(metadata.Resources.GetValueOrDefault("spa-client-script") ?? "");
            spaClientScriptTemplate.AddParameters(supportedParameters);

            return new ScriptOptions
            {
                BrowserAgentScriptUri = browserAgentScriptTemplate.Resolve(),
                HttpMessageUri = httpMessageTemplate.Resolve(),
                HudClientScriptUri = hudClientScriptTemplate.Resolve(),
                MetadataUri = metadataTemplate.Resolve(),
                SpaClientScriptUri = spaClientScriptTemplate.Resolve()
            };
        }
    }
}
