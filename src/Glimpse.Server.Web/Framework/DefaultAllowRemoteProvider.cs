using Microsoft.Extensions.OptionsModel;

namespace Glimpse.Server.Web
{
    public class DefaultAllowRemoteProvider : IAllowRemoteProvider
    {
        public DefaultAllowRemoteProvider(IOptions<GlimpseServerWebOptions> optionsAccessor)
        {
            AllowRemote = optionsAccessor.Value.AllowRemote; 
        }
        
        public bool AllowRemote { get; }
    }
}