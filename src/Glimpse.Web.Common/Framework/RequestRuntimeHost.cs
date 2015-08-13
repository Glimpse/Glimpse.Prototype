using System;
using Microsoft.AspNet.Http;
using System.Collections.Generic;
using Microsoft.AspNet.Builder;

namespace Glimpse.Web
{
    public class RequestRuntimeHost
    {
        private readonly IEnumerable<IRequestAuthorizer> _requestAuthorizers;
        //private readonly IEnumerable<IRequestRuntime> _requestRuntimes;
        //private readonly IEnumerable<IRequestHandler> _requestHandlers;

        public RequestRuntimeHost(IRequestAuthorizerProvider requestAuthorizerProvider) //, IRequestRuntimeProvider requestRuntimesProvider, IRequestHandlerProvider requestHandlersProvider)
        {
            _requestAuthorizers = requestAuthorizerProvider.Authorizers;
            //_requestRuntimes = requestRuntimesProvider.Runtimes; 
            //_requestHandlers = requestHandlersProvider.Handlers; 
        }

        public bool Authorized(HttpContext context)
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
        
        //public void Begin(HttpContext context)
        //{
        //    foreach (var requestRuntime in _requestRuntimes)
        //    {
        //        requestRuntime.Begin(context);
        //    }
        //}

        //public bool TryGetHandle(HttpContext context, out IRequestHandler handeler)
        //{
        //    foreach (var requestHandler in _requestHandlers)
        //    {
        //        if (requestHandler.WillHandle(context))
        //        {
        //            handeler = requestHandler;
        //            return true;
        //        }
        //    }

        //    handeler = null;
        //    return false;
        //}

        //public void End(HttpContext context)
        //{
        //    foreach (var requestRuntime in _requestRuntimes)
        //    {
        //        requestRuntime.End(context);
        //    }
        //}
    }







    public interface IDynamicMiddlewareComposer
    {
        void Register(IApplicationBuilder applicationBuilder);
    }

    public interface IResourceMiddlewareComposer : IDynamicMiddlewareComposer
    {
    }

    public interface ILogicMiddlewareComposer : IDynamicMiddlewareComposer
    {
    }

    public class TestResourceMiddlewareComposer : IResourceMiddlewareComposer
    {
        public void Register(IApplicationBuilder appBuilder)
        {
            appBuilder.Map("/test", newAppBuilder => newAppBuilder.Run(async context => await context.Response.WriteAsync("Agent!")));
        }
    }
    public class HeaderLogicMiddlewareComposer : ILogicMiddlewareComposer
    {
        public void Register(IApplicationBuilder appBuilder)
        {
            appBuilder.Use(async (context, next) =>
            {
                context.Response.Headers.Set("GLIMPSE", Guid.NewGuid().ToString());
                await next();
            });
        }
    }
}