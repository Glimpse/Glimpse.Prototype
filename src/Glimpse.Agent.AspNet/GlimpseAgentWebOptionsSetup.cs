using System;
using Glimpse.Agent;
using Microsoft.Extensions.OptionsModel;

namespace Glimpse.Agent
{
    public class GlimpseAgentWebOptionsSetup : ConfigureOptions<GlimpseAgentWebOptions>
    {
        public GlimpseAgentWebOptionsSetup() : base(ConfigureGlimpseAgentWebOptions)
        {
        }

        public static void ConfigureGlimpseAgentWebOptions(GlimpseAgentWebOptions options)
        {
            // Set up IgnoredUris
            options.IgnoredUris.AddCompiled("^/__browserLink/requestData");
            options.IgnoredUris.AddCompiled("^/Glimpse");
            options.IgnoredUris.AddCompiled("^/favicon.ico");
        }
    }
}