using Microsoft.Extensions.OptionsModel;

namespace Glimpse.Server
{
    public class GlimpseServerOptionsSetup : ConfigureOptions<GlimpseServerOptions>
    {
        public GlimpseServerOptionsSetup() : base(ConfigureGlimpseServerWebOptions)
        {
        }

        public static void ConfigureGlimpseServerWebOptions(GlimpseServerOptions options)
        {
            // TODO: Setup different settings here
        }
    }
}