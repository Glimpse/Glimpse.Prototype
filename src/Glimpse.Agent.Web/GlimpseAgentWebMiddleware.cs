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
        private readonly IContextData<MessageContext> _contextData;
        private readonly IEnumerable<IRequestIgnorer> _requestIgnorePolicies;

        public GlimpseAgentWebMiddleware(RequestDelegate next, IApplicationBuilder app, IContextData<MessageContext> contextData, IRequestIgnorerProvider requestIgnorerProvider, IInspectorStartupProvider inspectorStartupProvider)
        {
            _contextData = contextData;
            _next = next;
            //_settings = BuildSettings(shouldIgnoreRequest);
            _requestIgnorePolicies = requestIgnorerProvider.Policies;
            _branch = BuildBranch(app, inspectorStartupProvider);
        }
        
        public async Task Invoke(HttpContext context)
        {
            if (ShouldProfile(context))
            {
                _contextData.Value = new MessageContext { Id = Guid.NewGuid(), Type = "Request" };

                await _branch(context);
            }
            else
            {
                await _next(context);
            }
        }

        private RequestDelegate BuildBranch(IApplicationBuilder app, IInspectorStartupProvider inspectorStartupProvider)
        {
            // create new pipeline
            var branchBuilder = app.New();
            foreach (var middlewareProfiler in inspectorStartupProvider.Startups)
            {
                middlewareProfiler.Configure(new InspectorBuilder(branchBuilder));
            }
            branchBuilder.Use(subNext => { return async ctx => await _next(ctx); });

            return branchBuilder.Build();
        }

        private ISettings BuildSettings(Func<bool> shouldIgnoreRequest)
        {
            // TODO: Need to find a way/better place for 
            var settings = new Settings();
            if (shouldIgnoreRequest != null)
            {
                settings.ShouldProfile = shouldIgnoreRequest;
            }

            return settings;
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
