
using Glimpse.Web.Common;
using System;
using System.Threading.Tasks;
using Glimpse;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection; 

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
            return app.UseGlimpse(null);
        }

        public static IApplicationBuilder UseGlimpse(this IApplicationBuilder app, Func<bool> shouldRun)
        {
            var typeActivator = app.ApplicationServices.GetService<ITypeService>(); 

            var resourceMiddlewares = typeActivator.Resolve<IResourceMiddleware>();
            var logicMiddlewares = typeActivator.Resolve<ILogicMiddleware>();

            // create new pipeline
            var branchBuilder = app.New();
            // run through logic
            foreach (var logicMiddleware in logicMiddlewares)
            {
                logicMiddleware.Register(branchBuilder);
            }
            // run through resource
            branchBuilder.MapUse("/glimpse", innerApp =>
            {
                foreach (var resourceMiddleware in resourceMiddlewares)
                {
                    resourceMiddleware.Register(innerApp);
                }
            }); 
            
            return app.Use(next => new GlimpseMiddleware(next, branchBuilder, shouldRun).Invoke);
        }
    }

    public interface IDynamicMiddleware
    {
        void Register(IApplicationBuilder applicationBuilder);
    }

    public interface IResourceMiddleware : IDynamicMiddleware
    {
    }

    public interface ILogicMiddleware : IDynamicMiddleware
    {
    }

    public class TestResourceMiddleware : IResourceMiddleware
    {
        public void Register(IApplicationBuilder appBuilder)
        {
            appBuilder.Map("/test", newAppBuilder => newAppBuilder.Run(async context => await context.Response.WriteAsync("Agent!")));
        }
    }
    public class HeaderLogicMiddleware : ILogicMiddleware
    {
        public void Register(IApplicationBuilder appBuilder)
        {
            appBuilder.Use(async (context, next) =>
            {
                context.Response.Headers.Set("GLIMPSE", Guid.NewGuid().ToString());
                await next();
            });
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