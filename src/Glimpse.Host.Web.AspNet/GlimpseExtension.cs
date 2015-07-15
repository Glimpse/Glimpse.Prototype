
using System;
using Glimpse.Web;
using Glimpse.Host.Web.AspNet;

namespace Microsoft.AspNet.Builder
{
    public static class GlimpseExtension
    {
        /// <summary>
        /// Adds a middleware that allows GLimpse to be registered into the system.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGlimpse(this IApplicationBuilder app)
        {
            return app.Use(next => new GlimpseMiddleware(next, app.ApplicationServices).Invoke);
        }

        public static IApplicationBuilder UseGlimpse(this IApplicationBuilder app, Func<IHttpContext, bool> shouldRun)
        {
            return app.Use(next => new GlimpseMiddleware(next, app.ApplicationServices, shouldRun).Invoke);
        }
    }
}