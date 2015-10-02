using Glimpse.Web;

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

            return new ScriptOptions
            {
                BrowserAgentScriptUri = metadata.Resources.GetValueOrDefault("browser-agent-script"),
                HttpMessageUri = metadata.Resources.GetValueOrDefault("agent-message"),
                HudClientScriptUri = metadata.Resources.GetValueOrDefault("hud-client-script"),
                MetadataUri = metadata.Resources.GetValueOrDefault("metadata")
            };
        }
    }
}
