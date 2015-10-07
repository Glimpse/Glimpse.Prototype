using Glimpse.Server;
using Microsoft.Extensions.OptionsModel;

namespace Glimpse.Server.Configuration
{
    public class DefaultAllowRemoteProvider : IAllowRemoteProvider
    {
        public DefaultAllowRemoteProvider(IOptions<GlimpseServerOptions> optionsAccessor)
        {
            AllowRemote = optionsAccessor.Value.AllowRemote; 
        }
        
        public bool AllowRemote { get; }
    }
}