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
        private RequestDelegate _branch;

        public GlimpseServerWebMiddleware(RequestDelegate next, IApplicationBuilder app, IExtensionProvider<IAllowClientAccess> authorizeClientProvider, IExtensionProvider<IAllowAgentAccess> authorizeAgentProvider, IExtensionProvider<IResourceStartup> resourceStartupsProvider, IExtensionProvider<IResource> resourceProvider, IResourceManager resourceManager)
        {
            _authorizeClients = authorizeClientProvider.Instances;
            _authorizeAgents = authorizeAgentProvider.Instances;
            _next = next;

            BuildBranch(app, resourceStartupsProvider.Instances, resourceProvider.Instances, resourceManager);
        }
        
        public async Task Invoke(HttpContext context)
        {
            await _branch(context);
        }

        public void BuildBranch(IApplicationBuilder app, IEnumerable<IResourceStartup> resourceStartups, IEnumerable<IResource> resources, IResourceManager resourceManager)
        {
            // create new pipeline
            var branchApp = app.New();
            branchApp.Map("/glimpse", innerBranchBuilder =>
            {
                // REGISTER: resource startups
                var resourceStartupCallbacks = new List<Tuple<RequestDelegate, ResourceType>>();
                foreach (var resourceStartup in resourceStartups)
                {
                    var subBranchBuilder = innerBranchBuilder.New();

                    var resourceBuilderStartup = new ResourceBuilder(subBranchBuilder, resourceManager);
                    resourceStartup.Configure(resourceBuilderStartup);
                    
                    resourceStartupCallbacks.Add(new Tuple<RequestDelegate, ResourceType>(subBranchBuilder.Build(), resourceStartup.Type));
                }
                // REGISTER: resources
                var resourceBuilder = new ResourceBuilder(innerBranchBuilder, resourceManager);
                foreach (var resource in resources)
                {
                    resourceBuilder.Run(resource.Name, resource.Parameters?.GenerateUriTemplate(), resource.Type, resource.Invoke);
                }
                
                innerBranchBuilder.Run(async context =>
                {
                    // RUN: resource startups
                    foreach (var resourceStartupCallback in resourceStartupCallbacks)
                    {
                        if (CanExecute(context, resourceStartupCallback.Item2))
                        {
                            await resourceStartupCallback.Item1(context);
                        }
                    }

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

            _branch = branchApp.Build();
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