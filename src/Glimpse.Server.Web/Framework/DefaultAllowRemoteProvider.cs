using Microsoft.Framework.OptionsModel;

namespace Glimpse.Server.Web
{
    public class DefaultAllowRemoteProvider : IAllowRemoteProvider
    {
        public DefaultAllowRemoteProvider(IOptions<GlimpseServerWebOptions> optionsAccessor)
        {
            AllowRemote = optionsAccessor.Options.AllowRemote; 
        }
        
        public bool AllowRemote { get; }
    }
}