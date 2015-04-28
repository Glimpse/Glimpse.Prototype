using System;
using Glimpse.Agent.Web.Options;
using Microsoft.Framework.OptionsModel;

namespace Glimpse.Agent.Web
{
    public class GlimpseAgentWebOptionsSetup : ConfigureOptions<GlimpseAgentWebOptions>
    {
        public GlimpseAgentWebOptionsSetup() : base(ConfigureGlimpseAgentWebOptions)
        {
            Order = -1000;
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