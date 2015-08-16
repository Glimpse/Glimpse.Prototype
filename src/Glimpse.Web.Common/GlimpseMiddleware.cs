using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Http;

namespace Glimpse.Web.Common
{
    public class GlimpseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RequestDelegate _branch; 
        private readonly ISettings _settings;
        private readonly IEnumerable<IRequestAuthorizer> _requestAuthorizers;
        
        public GlimpseMiddleware(RequestDelegate next, IApplicationBuilder app, IRequestAuthorizerProvider requestAuthorizerProvider,  IMiddlewareLogicComposerProvider middlewareLogicComposersProvider, IMiddlewareResourceComposerProvider middlewareResourceComposerProvider)
        {
            _next = next; 
            //_settings = BuildSettings(userHasAccess);
            _requestAuthorizers = requestAuthorizerProvider.Authorizers;
            
            // create new pipeline
            var branchBuilder = app.New();
            branchBuilder.Map("/glimpse", innerApp =>
            {
                // run through logic
                foreach (var middlewareLogic in middlewareLogicComposersProvider.Logic)
                {
                    middlewareLogic.Register(innerApp);
                }
                // run through resource
                foreach (var middlewareResource in middlewareResourceComposerProvider.Resources)
                {
                    middlewareResource.Register(innerApp);
                }
            });
            branchBuilder.Use(subNext => { return async ctx => await next(ctx); });

            _branch = branchBuilder.Build();
        }

        // TODO: Look at pushing the workings of this into MasterRequestRuntime
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
        
        private ISettings BuildSettings(Func<bool> userHasAccess)
        {
            // TODO: Need to find a way/better place for 
            var settings = new Settings();
            if (userHasAccess != null)
            {
                settings.ShouldProfile = userHasAccess;
            }

            return settings;
        }

        private bool ShouldExecute(HttpContext context)
        {
            foreach (var requestAuthorizer in _requestAuthorizers)
            {
                var allowed = requestAuthorizer.AllowUser(context);
                if (!allowed)
                {
                    return false;
                }
            }

            return true;
        }
    }
}