using Microsoft.Framework.OptionsModel;

namespace Glimpse
{
    public class ScriptOptionsSetup : ConfigureOptions<ScriptOptions>
    {
        public ScriptOptionsSetup(IMetadata metadata) : base(options => { ConfigureGlimpseOptions(options, metadata); })
        {
        }

        public static void ConfigureGlimpseOptions(ScriptOptions options, IMetadata metadata)
        {
            options.BrowserAgentScriptUri = metadata.Resources.GetValueOrDefault("browser-agent-script");
            options.HttpMessageUri = metadata.Resources.GetValueOrDefault("http-message");
            options.HudClientScriptUri = metadata.Resources.GetValueOrDefault("hud-client-script");
            options.MetadataUri = metadata.Resources.GetValueOrDefault("metadata");
        }
    }
}