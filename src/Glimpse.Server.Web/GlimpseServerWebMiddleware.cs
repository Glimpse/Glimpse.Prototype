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
        private readonly IEnumerable<IClientAuthorizer> _clientAuthorizers;
        
        public GlimpseServerWebMiddleware(RequestDelegate next, IApplicationBuilder app, IExtensionProvider<IClientAuthorizer> clientAuthorizerProvider, IExtensionProvider<IResourceStartup> resourceStartupsProvider, IResourceManager resourceManager)
        {
            _next = next; 
            _clientAuthorizers = clientAuthorizerProvider.Instances;
            _branch = BuildBranch(app, resourceStartupsProvider.Instances, resourceManager);
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

        public RequestDelegate BuildBranch(IApplicationBuilder app, IEnumerable<IResourceStartup> resourceStartups, IResourceManager resourceManager)
        {
            // create new pipeline
            var branchBuilder = app.New();
            branchBuilder.Map("/glimpse", innerApp =>
            {
                // register resource startups
                var resourceBuilder = new ResourceBuilder(app, resourceManager);
                foreach (var resourceStartup in resourceStartups)
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
        
        private bool ShouldExecute(HttpContext context)
        {
            foreach (var clientAuthorizer in _clientAuthorizers)
            {
                var allowed = clientAuthorizer.AllowUser(context);
                if (!allowed)
                {
                    return false;
                }
            }

            return true;
        }
    }
}