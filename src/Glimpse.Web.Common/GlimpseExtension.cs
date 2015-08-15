
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
            var middlewareLogicComposers = app.ApplicationServices.GetService<IMiddlewareLogicComposerProvider>().Logic;
            var middlewareResourceComposers = app.ApplicationServices.GetService<IMiddlewareResourceComposerProvider>().Resources;
            
            // create new pipeline
            var branchBuilder = app.New();
            branchBuilder.Map("/glimpse", innerApp =>
            {
                // run through logic
                foreach (var middlewareLogic in middlewareLogicComposers)
                {
                    middlewareLogic.Register(innerApp);
                }
                // run through resource
                foreach (var middlewareResource in middlewareResourceComposers)
                {
                    middlewareResource.Register(innerApp);
                }
            }); 

            return app.Use(next => new GlimpseMiddleware(next, branchBuilder, shouldRun).Invoke);
        }
    } 
}