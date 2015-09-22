using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;

namespace Glimpse.Server.Web
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
            var connectionFeature = context.Features.Get<IHttpConnectionFeature>();

            // TODO: MUST BE REMOVED!!!!
            var bypass = connectionFeature == null;
            // TODO: MUST BE REMOVED!!!!

            return bypass || _allowRemoteProvider.AllowRemote || (connectionFeature != null && connectionFeature.IsLocal);
        }
    }
}