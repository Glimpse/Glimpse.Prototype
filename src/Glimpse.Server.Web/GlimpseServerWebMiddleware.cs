using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;

namespace Glimpse.Server.Web
{
    public class GlimpseServerWebMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RequestDelegate _branch; 
        private readonly IEnumerable<IAuthorizeClient> _authorizeClients;
        private readonly IEnumerable<IAuthorizeAgent> _authorizeAgents;

        public GlimpseServerWebMiddleware(RequestDelegate next, IApplicationBuilder app, IExtensionProvider<IAuthorizeClient> authorizeClientProvider, IExtensionProvider<IAuthorizeAgent> authorizeAgentProvider, IExtensionProvider<IResourceStartup> resourceStartupsProvider, IResourceManager resourceManager)
        {
            _next = next;
            _authorizeClients = authorizeClientProvider.Instances;
            _authorizeAgents = authorizeAgentProvider.Instances;
            _branch = BuildBranch(app, resourceStartupsProvider.Instances, resourceManager);
        }
        
        public async Task Invoke(HttpContext context)
        {
            await _branch(context);
        }

        public RequestDelegate BuildBranch(IApplicationBuilder app, IEnumerable<IResourceStartup> resourceStartups, IResourceManager resourceManager)
        {
            // create new pipeline
            var branchBuilder = app.New();
            branchBuilder.Map("/glimpse", innerBranchBuilder =>
            {
                // REGISTER: resource startups
                var resourceBuilder = new ResourceBuilder(innerBranchBuilder, resourceManager);
                foreach (var resourceStartup in resourceStartups)
                {
                    resourceStartup.Configure(resourceBuilder);
                }

                // RUN: our own pipline after the resource have had a chance to intercept
                //     it if they want want. Normally they wont tap the underlying appBuider 
                //     directly but it is possible and the following is terminating
                innerBranchBuilder.Run(async context =>
                {
                    var result = resourceManager.Match(context);
                    if (result != null)
                    {
                        if (CanExecute(context, result.Type))
                        {
                            await result.Resource(context, result.Paramaters);
                        }
                        else
                        {
                            // TODO: Review, do we want a 401, 404 or continue users pipeline 
                            context.Response.StatusCode = 401;
                        }
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

        public bool CanExecute(HttpContext context, ResourceType type)
        {
            return ResourceType.Agent == type ? ShouldAllowAgent(context) : ShouldAllowUser(context);
        }
        
        private bool ShouldAllowUser(HttpContext context)
        {
            foreach (var authorizeClient in _authorizeClients)
            {
                var allowed = authorizeClient.AllowUser(context);
                if (!allowed)
                {
                    return false;
                }
            }

            return true;
        }

        // TODO: Need to wire up
        private bool ShouldAllowAgent(HttpContext context)
        {
            foreach (var authorizeAgent in _authorizeAgents)
            {
                var allowed = authorizeAgent.AllowAgent(context);
                if (!allowed)
                {
                    return false;
                }
            }

            return true;
        }
    }
}