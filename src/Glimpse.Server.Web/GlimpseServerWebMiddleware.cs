using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;

namespace Glimpse.Server.Web
{
    public class GlimpseServerWebMiddleware
    {
        private readonly IEnumerable<IAllowClientAccess> _authorizeClients;
        private readonly IEnumerable<IAllowAgentAccess> _authorizeAgents;
        private readonly RequestDelegate _next;
        private readonly RequestDelegate _branch;

        public GlimpseServerWebMiddleware(RequestDelegate next, IApplicationBuilder app, IExtensionProvider<IAllowClientAccess> authorizeClientProvider, IExtensionProvider<IAllowAgentAccess> authorizeAgentProvider, IExtensionProvider<IResourceStartup> resourceStartupsProvider, IExtensionProvider<IResource> resourceProvider, IResourceManager resourceManager)
        {
            _authorizeClients = authorizeClientProvider.Instances;
            _authorizeAgents = authorizeAgentProvider.Instances;

            _next = next;
            _branch = BuildBranch(app, resourceStartupsProvider.Instances, resourceProvider.Instances, resourceManager);
        }
        
        public async Task Invoke(HttpContext context)
        {
            await _branch(context);
        }

        public RequestDelegate BuildBranch(IApplicationBuilder app, IEnumerable<IResourceStartup> resourceStartups, IEnumerable<IResource> resources, IResourceManager resourceManager)
        {
            var branchApp = app.New();
            branchApp.Map("/glimpse", glimpseApp =>
            {
                // REGISTER: resource startups
                foreach (var resourceStartup in resourceStartups)
                {
                    var startupApp = glimpseApp.New();

                    var resourceBuilderStartup = new ResourceBuilder(startupApp, resourceManager);
                    resourceStartup.Configure(resourceBuilderStartup);

                    glimpseApp.Use(next =>
                    {
                        startupApp.Use(a => { return async ctx => await next(ctx); });

                        var startupBranch = startupApp.Build();

                        return async context =>
                        {
                            if (CanExecute(context, resourceStartup.Type))
                            {
                                await startupBranch(context);
                            }
                        };
                    });
                }
                // REGISTER: resources
                var resourceBuilder = new ResourceBuilder(glimpseApp, resourceManager);
                foreach (var resource in resources)
                {
                    resourceBuilder.Run(resource.Name, resource.Parameters?.GenerateUriTemplate(), resource.Type,
                        resource.Invoke);
                }

                glimpseApp.Run(async context =>
                {
                    // RUN: resources
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
                });
            });
            branchApp.Use(subNext => { return async ctx => await _next(ctx); });

            return branchApp.Build();
        }

        public bool CanExecute(HttpContext context, ResourceType type)
        {
            return ResourceType.Agent == type ? AllowAgentAccess(context) : AllowClientAccess(context);
        }
        
        private bool AllowClientAccess(HttpContext context)
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
        
        private bool AllowAgentAccess(HttpContext context)
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