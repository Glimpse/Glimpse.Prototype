﻿using System;
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

        public GlimpseAgentWebMiddleware(RequestDelegate next, IApplicationBuilder app, IRequestIgnorerManager requestIgnorerManager, IInspectorStartupManager inspectorStartupManager)
        {
            _next = next;
            _requestIgnorerManager = requestIgnorerManager;
            _branch = inspectorStartupManager.BuildInspectorBranch(next, app);
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
