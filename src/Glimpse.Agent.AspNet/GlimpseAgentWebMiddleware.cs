using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace Glimpse.Agent.AspNet
{
    public class GlimpseAgentWebMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RequestDelegate _branch;
        private readonly IRequestIgnorerManager _requestIgnorerManager;

        public GlimpseAgentWebMiddleware(RequestDelegate next, IApplicationBuilder app, IRequestIgnorerManager requestIgnorerManager, IInspectorFunctionManager inspectorFunctionManager)
        {
            _next = next;
            _requestIgnorerManager = requestIgnorerManager;
            _branch = inspectorFunctionManager.BuildInspectorBranch(next, app);
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
    }
}
