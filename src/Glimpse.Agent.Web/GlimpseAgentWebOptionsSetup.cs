using System;
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
            // TODO: Setup anything that is order dependent 
        }
    }
}