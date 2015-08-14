
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
            // run through logic
            foreach (var middlewareLogic in middlewareLogicComposers)
            {
                middlewareLogic.Register(branchBuilder);
            }
            // run through resource
            branchBuilder.MapUse("/glimpse", innerApp =>
            {
                foreach (var middlewareResource in middlewareResourceComposers)
                {
                    middlewareResource.Register(innerApp);
                }
            });

            return app.Use(next => new GlimpseMiddleware(next, branchBuilder, shouldRun).Invoke);
        }
    }






    public static class MapUseExtensions
    {
        public static IApplicationBuilder MapUse(this IApplicationBuilder app, PathString pathMatch, Action<IApplicationBuilder> configuration)
        {
            if (pathMatch.HasValue && pathMatch.Value.EndsWith("/", StringComparison.Ordinal))
            {
                throw new ArgumentException("The path must not end with a '/'", nameof(pathMatch));
            }

            // create branch
            var branchBuilder = app.New();
            configuration(branchBuilder);

            return app.Use(next => new MapUseMiddleware(next, branchBuilder, pathMatch).Invoke);
        }
    }

    public class MapUseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly PathString _pathMatch;
        private readonly RequestDelegate _branch;

        public MapUseMiddleware(RequestDelegate next, IApplicationBuilder branchBuilder, PathString pathMatch)
        {
            _next = next;
            _pathMatch = pathMatch;

            // this is registered at the end of the pipeline
            branchBuilder.Use(subNext => { return async ctx => await next(ctx); });

            _branch = branchBuilder.Build();
        }

        public async Task Invoke(HttpContext context)
        {
            PathString path = context.Request.Path;
            PathString remainingPath;
            if (path.StartsWithSegments(_pathMatch, out remainingPath))
            {
                // Update the path
                PathString pathBase = context.Request.PathBase;
                context.Request.PathBase = pathBase + _pathMatch;
                context.Request.Path = remainingPath;

                await _branch(context);

                context.Request.PathBase = pathBase;
                context.Request.Path = path;
            }
            else
            {
                await _next(context);
            }
        }
    }
}