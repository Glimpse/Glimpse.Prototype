using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Http;

namespace Glimpse.Server.Web
{
    public class GlimpseServerWebMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RequestDelegate _branch; 
        private readonly ISettings _settings;
        private readonly IEnumerable<IRequestAuthorizer> _requestAuthorizers;
        
        public GlimpseServerWebMiddleware(RequestDelegate next, IApplicationBuilder app, IRequestAuthorizerProvider requestAuthorizerProvider,  IResourceStartupProvider resourceStartupsProvider, IResourceManager resourceManager)
        {
            _next = next; 
            //_settings = BuildSettings(userHasAccess);
            _requestAuthorizers = requestAuthorizerProvider.Authorizers;
            _branch = BuildBranch(app, resourceStartupsProvider, resourceManager);
        }
        
        public async Task Invoke(HttpContext context)
        {
            if (ShouldExecute(context))
            { 
                await _branch(context);
            }
            else
            {
                await _next(context);
            }
        }

        public RequestDelegate BuildBranch(IApplicationBuilder app, IResourceStartupProvider resourceStartupsProvider, IResourceManager resourceManager)
        {
            // create new pipeline
            var branchBuilder = app.New();
            branchBuilder.Map("/glimpse", innerApp =>
            {
                // register resource startups
                var resourceBuilder = new ResourceBuilder(app, resourceManager);
                foreach (var resourceStartup in resourceStartupsProvider.Startups)
                {
                    resourceStartup.Configure(resourceBuilder);
                }

                // run our own pipline after the resource have had a chance to intercept
                //     it if they want want. Normally they wont tap the underlying appBuider 
                //     directly but it is possible and the following is terminating
                innerApp.Run(async context =>
                {
                    var result = resourceManager.Match(context);
                    if (result != null)
                    {
                        await result.Resource(context, result.Paramaters);
                    }
                    else
                    {
                        context.Response.StatusCode = 404;
                    }
                });
            });
            branchBuilder.Use(subNext => { return async ctx => await _next(ctx); });

            return branchBuilder.Build();
        }

        private ISettings BuildSettings(Func<bool> userHasAccess)
        {
            // TODO: Need to find a way/better place for 
            var settings = new Settings();
            if (userHasAccess != null)
            {
                settings.ShouldProfile = userHasAccess;
            }

            return settings;
        }

        private bool ShouldExecute(HttpContext context)
        {
            foreach (var requestAuthorizer in _requestAuthorizers)
            {
                var allowed = requestAuthorizer.AllowUser(context);
                if (!allowed)
                {
                    return false;
                }
            }

            return true;
        }
    }
}