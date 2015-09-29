using Glimpse.Web;

namespace Glimpse.Server.Web
{
    public class DefaultScriptOptionsProvider : IScriptOptionsProvider
    {
        public DefaultScriptOptionsProvider(IMetadataProvider metadataProvider)
        {
            Metadata = metadataProvider.BuildInstance();
        }

        private Metadata Metadata { get; }

        public ScriptOptions BuildInstance()
        {
            var options = new ScriptOptions();

            options.BrowserAgentScriptUri = Metadata.Resources.GetValueOrDefault("browser-agent-script");
            options.HttpMessageUri = Metadata.Resources.GetValueOrDefault("http-message");
            options.HudClientScriptUri = Metadata.Resources.GetValueOrDefault("hud-client-script");
            options.MetadataUri = Metadata.Resources.GetValueOrDefault("metadata");

            return options;
        }
    }
}
