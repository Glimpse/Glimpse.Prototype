
using Glimpse.Web.Common;
using System;
using System.Threading.Tasks;
using Glimpse;
using Glimpse.Web;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection; 

namespace Microsoft.AspNet.Builder
{
    public static class GlimpseExtension
    {
        public static IApplicationBuilder UseGlimpse(this IApplicationBuilder app)
        {
            return app.UseGlimpse(null);
        }

        public static IApplicationBuilder UseGlimpse(this IApplicationBuilder app, Func<bool> shouldRun)
        {
            return app.UseMiddleware<GlimpseMiddleware>(app);
        }
    } 
}