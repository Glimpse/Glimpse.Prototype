using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Glimpse.Server.Configuration
{
    public class AllowClientAccessRemote : IAllowClientAccess
    {
        private readonly IAllowRemoteProvider _allowRemoteProvider;

        public AllowClientAccessRemote(IAllowRemoteProvider allowRemoteProvider)
        {
            _allowRemoteProvider = allowRemoteProvider;
        }

        public bool AllowUser(HttpContext context)
        {
            // TODO: need to fix this logic up since IsLocal isn't available and wouldn't work

            var connectionFeature = context.Features.Get<IHttpConnectionFeature>();
            return _allowRemoteProvider.AllowRemote; //  || (connectionFeature != null && connectionFeature.IsLocal);
        }
    }
}