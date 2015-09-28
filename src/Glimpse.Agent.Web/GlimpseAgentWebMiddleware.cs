using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace Glimpse.Agent.Web
{
    public class GlimpseAgentWebMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RequestDelegate _branch;
        private readonly ISettings _settings;
        private readonly IRequestIgnorerManager _requestIgnorerManager;

        public GlimpseAgentWebMiddleware(RequestDelegate next, IApplicationBuilder app, IRequestIgnorerManager requestIgnorerManager, IExtensionProvider<IInspectorStartup> inspectorStartupProvider)
        {
            _next = next;
            _requestIgnorerManager = requestIgnorerManager;
            _branch = BuildBranch(app, inspectorStartupProvider.Instances);
        }
        
        public async Task Invoke(HttpContext context)
        {
            if (!_requestIgnorerManager.ShouldIgnore(context))
            {
                await _branch(context);
            }
            else
            {
                await _next(context);
            }
        }

        private RequestDelegate BuildBranch(IApplicationBuilder app, IEnumerable<IInspectorStartup> inspectorStartupProvider)
        {
            // create new pipeline
            var branchBuilder = app.New();
            foreach (var middlewareProfiler in inspectorStartupProvider)
            {
                middlewareProfiler.Configure(new InspectorBuilder(branchBuilder));
            }
            branchBuilder.Use(subNext => { return async ctx => await _next(ctx); });

            return branchBuilder.Build();
        }
    }
}
