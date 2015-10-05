using System;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.OptionsModel;

namespace Glimpse.Server.Web.Framework
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
