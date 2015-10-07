using Microsoft.Extensions.OptionsModel;

namespace Glimpse.Server
{
    public class GlimpseServerWebOptionsSetup : ConfigureOptions<GlimpseServerWebOptions>
    {
        public GlimpseServerWebOptionsSetup() : base(ConfigureGlimpseServerWebOptions)
        {
        }

        public static void ConfigureGlimpseServerWebOptions(GlimpseServerWebOptions options)
        {
            // TODO: Setup different settings here
        }
    }
}