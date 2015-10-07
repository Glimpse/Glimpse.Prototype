using Glimpse.Agent.Configuration;
using Microsoft.Extensions.OptionsModel;

namespace Glimpse.Agent
{
    public class GlimpseAgentOptionsSetup : ConfigureOptions<GlimpseAgentOptions>
    {
        public GlimpseAgentOptionsSetup() : base(ConfigureGlimpseAgentWebOptions)
        {
        }

        public static void ConfigureGlimpseAgentWebOptions(GlimpseAgentOptions options)
        {
            // Set up IgnoredUris
            options.IgnoredUris.AddCompiled("^/__browserLink/requestData");
            options.IgnoredUris.AddCompiled("^/Glimpse");
            options.IgnoredUris.AddCompiled("^/favicon.ico");
        }
    }
}