using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Glimpse.Web;
using System;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Http;

namespace Glimpse.Web.Common
{
    public class GlimpseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RequestDelegate _branch;
        private readonly RequestRuntimeHost _runtime;
        private readonly ISettings _settings;
        private readonly IContextData<MessageContext> _contextData;

        public GlimpseMiddleware(RequestDelegate next, IApplicationBuilder branchBuilder)
            : this(next, branchBuilder, null)
        {
        }

        public GlimpseMiddleware(RequestDelegate next, IApplicationBuilder branchBuilder, Func<bool> shouldRun)
        {
            _next = next;
            
            // this is registered at the end of the pipeline
            branchBuilder.Use(subNext => { return async ctx => await next(ctx); });

            _branch = branchBuilder.Build();



            var typeActivator = branchBuilder.ApplicationServices.GetService<ITypeActivator>();
             
            _runtime = typeActivator.CreateInstance<RequestRuntimeHost>(); 
            _contextData = new ContextData<MessageContext>();

            // TODO: Need to find a way/better place for 
            var settings = new Settings();
            if (shouldRun != null)
            {
                settings.ShouldProfile = shouldRun;
            }
            _settings = settings;
        }

        // TODO: Look at pushing the workings of this into MasterRequestRuntime
        public async Task Invoke(HttpContext context)
        {
            if (_runtime.Authorized(context))
            {
                // TODO: This is the wrong place for this, AgentRuntime isn't garenteed to execute first
                _contextData.Value = new MessageContext { Id = Guid.NewGuid(), Type = "Request" };
                
                await _branch(context);
            }
            else
            {
                await _next(context);
            }
        }
    }
}