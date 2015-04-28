using System;
using Glimpse.Server.Options;
using Glimpse.Web;

namespace Glimpse.Server.Framework
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