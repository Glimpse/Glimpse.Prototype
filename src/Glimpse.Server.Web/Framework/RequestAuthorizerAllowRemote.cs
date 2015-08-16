using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;

namespace Glimpse.Server.Web
{
    public class RequestAuthorizerAllowRemote : IRequestAuthorizer
    {
        private readonly IAllowRemoteProvider _allowRemoteProvider;

        public RequestAuthorizerAllowRemote(IAllowRemoteProvider allowRemoteProvider)
        {
            _allowRemoteProvider = allowRemoteProvider;
        }

        public bool AllowUser(HttpContext context)
        {
            var connectionFeature = context.GetFeature<IHttpConnectionFeature>();
            return _allowRemoteProvider.AllowRemote || (connectionFeature != null && connectionFeature.IsLocal);
        }
    }
}