using System;
using Microsoft.AspNet.Http;
using Microsoft.Framework.OptionsModel;

namespace Glimpse.Server.Web.Framework
{
    public class AllowClientAccessOptions : IAllowClientAccess
    {
        private readonly Func<HttpContext, bool> _canAccess;

        public AllowClientAccessOptions(IOptions<GlimpseServerWebOptions> optionsAccessor)
        {
            _canAccess = optionsAccessor.Value.CanAccessClient;
        }

        public bool AllowUser(HttpContext context)
        {
            return _canAccess != null ? _canAccess(context) : true;
        }
    }
}
