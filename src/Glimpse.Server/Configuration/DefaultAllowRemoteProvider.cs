using Glimpse.Server.Web;
using Microsoft.Extensions.OptionsModel;

namespace Glimpse.Server.Configuration
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