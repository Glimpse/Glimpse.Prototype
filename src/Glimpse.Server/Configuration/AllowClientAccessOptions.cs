using System;
using Glimpse.Server.Web;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.OptionsModel;

namespace Glimpse.Server.Configuration
{
    public class AllowClientAccessOptions : IAllowClientAccess
    {
        private readonly Func<HttpContext, bool> _allowAccess;

        public AllowClientAccessOptions(IOptions<GlimpseServerWebOptions> optionsAccessor)
        {
            _allowAccess = optionsAccessor.Value.AllowClientAccess;
        }

        public bool AllowUser(HttpContext context)
        {
            return _allowAccess != null ? _allowAccess(context) : true;
        }
    }
}
