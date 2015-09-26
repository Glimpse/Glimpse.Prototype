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
        private readonly IEnumerable<IRequestIgnorer> _requestIgnorePolicies;

        public GlimpseAgentWebMiddleware(RequestDelegate next, IApplicationBuilder app, IExtensionProvider<IRequestIgnorer> requestIgnorerProvider, IExtensionProvider<IInspectorStartup> inspectorStartupProvider)
        {
            _next = next;
            _requestIgnorePolicies = requestIgnorerProvider.Instances;
            _branch = BuildBranch(app, inspectorStartupProvider.Instances);
        }
        
        public async Task Invoke(HttpContext context)
        {
            if (ShouldProfile(context))
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
        
        private bool ShouldProfile(HttpContext context)
        {
            if (_requestIgnorePolicies.Any())
            {
                foreach (var policy in _requestIgnorePolicies)
                {
                    if (policy.ShouldIgnore(context))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
