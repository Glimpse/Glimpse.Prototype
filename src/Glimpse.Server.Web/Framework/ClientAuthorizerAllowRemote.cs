using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;

namespace Glimpse.Server.Web
{
    public class ClientAuthorizerAllowRemote : IClientAuthorizer
    {
        private readonly IAllowRemoteProvider _allowRemoteProvider;

        public ClientAuthorizerAllowRemote(IAllowRemoteProvider allowRemoteProvider)
        {
            _allowRemoteProvider = allowRemoteProvider;
        }

        public bool AllowUser(HttpContext context)
        {
            var connectionFeature = context.Features.Get<IHttpConnectionFeature>();
            return _allowRemoteProvider.AllowRemote || (connectionFeature != null && connectionFeature.IsLocal);
        }
    }
}