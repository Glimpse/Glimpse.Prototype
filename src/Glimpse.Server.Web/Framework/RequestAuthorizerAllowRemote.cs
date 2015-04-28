using System; 
using Glimpse.Web;

namespace Glimpse.Server
{
    public class RequestAuthorizerAllowRemote : IRequestAuthorizer
    {
        private readonly IAllowRemoteProvider _allowRemoteProvider;

        public RequestAuthorizerAllowRemote(IAllowRemoteProvider allowRemoteProvider)
        {
            _allowRemoteProvider = allowRemoteProvider;
        }

        public bool AllowUser(IHttpContext context)
        {
            return _allowRemoteProvider.AllowRemote || context.Request.IsLocal;
        }
    }
}