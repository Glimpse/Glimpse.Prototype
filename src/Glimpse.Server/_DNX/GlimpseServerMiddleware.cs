#if DNX
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System.Collections.Generic;
using Glimpse.Initialization;
using Glimpse.Server.Configuration;
using Glimpse.Server.Internal;
using Glimpse.Server.Resources;
using Microsoft.Extensions.OptionsModel;

namespace Glimpse.Server
{
    public class GlimpseServerMiddleware
    {
        private readonly IResourceRuntimeManager _resourceRuntimeManager;
        private readonly IResourceAuthorization _resourceAuthorization;
        private readonly GlimpseServerOptions _serverOptions;
        private readonly RequestDelegate _next;
        private readonly RequestDelegate _branch;

        public GlimpseServerMiddleware(RequestDelegate next, IApplicationBuilder app, IExtensionProvider<IResourceStartup> resourceStartupsProvider, IResourceAuthorization resourceAuthorization, IResourceRuntimeManager resourceRuntimeManager, IResourceManager resourceManager, IOptions<GlimpseServerOptions> serverOptions)
        {
            _resourceAuthorization = resourceAuthorization;
            _resourceRuntimeManager = resourceRuntimeManager;
            _serverOptions = serverOptions.Value;

            _next = next;
            _branch = BuildBranch(app, resourceStartupsProvider.Instances, resourceManager);
        }
        
        public async Task Invoke(HttpContext context)
        {
            await _branch(context);
        }

        public RequestDelegate BuildBranch(IApplicationBuilder app, IEnumerable<IResourceStartup> resourceStartups, IResourceManager resourceManager)
        {
            var branchApp = app.New();
            branchApp.Map($"/{_serverOptions.BasePath}", glimpseApp =>
            {
                // REGISTER: resource startups
                foreach (var resourceStartup in resourceStartups)
                {
                    var startupApp = glimpseApp.New();

                    var resourceBuilderStartup = new ResourceBuilder(startupApp, resourceManager);
                    resourceStartup.Configure(resourceBuilderStartup);

                    glimpseApp.Use(next =>
                    {
                        startupApp.Run(next);

                        var startupBranch = startupApp.Build();

                        return context =>
                        {
                            if (_resourceAuthorization.CanExecute(context, resourceStartup.Type))
                            {
                                return startupBranch(context);
                            }

                            return next(context);
                        };
                    });
                }

                // REGISTER: resources
                _resourceRuntimeManager.Register();

                // RUN: resources
                glimpseApp.Run(async context => { await _resourceRuntimeManager.ProcessRequest(context); });
            });
            branchApp.Use(subNext => { return async ctx => await _next(ctx); });

            return branchApp.Build();
        }
    }
}
#endif