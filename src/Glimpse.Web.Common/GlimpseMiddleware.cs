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
                
                    /*
                _runtime.Begin(context);

                var handler = (IRequestHandler)null;
                if (_runtime.TryGetHandle(context, out handler))
                {
                    await handler.Handle(context);
                }
                else
                {
                    await _next(context);
                }

                // TODO: This doesn't work correctly :( (headers)
                _runtime.End(context);
                */

                await _branch(context);
            }
            else
            {
                await _next(context);
            }

            /*

            //TODO: Logic should probably be more like this.
            //      Problem is that WebCommon shouldn't know about Profile.
            //      Have to think about this more. Mayber there is a pattern here???

            var handeled = false;

            var handler = (IRequestHandler) null;
            if (_runtime.TryGetHandle(newContext, out handler)
                && _runtime.AuthorizedRequest(newContext))  // Is internal Glimpse request
            { 
                handeled = true;
                    
                await _runtime.HandleBegin(newContext); 
                await handler.Handle(newContext); 
                await _runtime.HandleEnd(newContext); 
            }
            else if (_runtime.IgnoredRequest(newContext)
            {
                handeled = true;
                    
                // TODO: This is the wrong place for this, AgentRuntime isn't garenteed to execute first
                _contextData.Value = new MessageContext {Id = Guid.NewGuid(), Type = "Request"};

                await _runtime.ProfileBegin(newContext); 
                await handler.Profile(newContext); 
                await _runtime.ProfileEnd(newContext); 
            }

            if (!handeled)
            { 
                await _next(context);
            }
            */
        }
    }
}