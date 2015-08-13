using System;
using Microsoft.AspNet.Http;
using System.Collections.Generic;
using Microsoft.AspNet.Builder;

namespace Glimpse.Web
{
    public class RequestRuntimeHost
    {
        private readonly IEnumerable<IRequestAuthorizer> _requestAuthorizers;
        private readonly IEnumerable<ILogicMiddleware> _logicMiddlewareContainers;
        private readonly IEnumerable<IResourceMiddleware> _resourceMiddlewareContainers;
        //private readonly IEnumerable<IRequestRuntime> _requestRuntimes;
        //private readonly IEnumerable<IRequestHandler> _requestHandlers;

        public RequestRuntimeHost(IRequestAuthorizerProvider requestAuthorizerProvider, ITypeService typeService) //, IRequestRuntimeProvider requestRuntimesProvider, IRequestHandlerProvider requestHandlersProvider)
        {
            _requestAuthorizers = requestAuthorizerProvider.Authorizers;
            // TODO: needs to be switched over to using providers
            _logicMiddlewareContainers = typeService.Resolve<ILogicMiddleware>();
            _resourceMiddlewareContainers = typeService.Resolve<IResourceMiddleware>();
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

        public IApplicationBuilder BuildBranchBuilder(IApplicationBuilder appBuilder)
        {
            // create new pipeline
            var branchBuilder = appBuilder.New();
            // run through logic
            foreach (var logicContainer in _logicMiddlewareContainers)
            {
                logicContainer.Register(branchBuilder);
            }
            // run through resource
            branchBuilder.MapUse("/glimpse", innerApp =>
            {
                foreach (var resourceContainer in _resourceMiddlewareContainers)
                {
                    resourceContainer.Register(innerApp);
                }
            });

            return branchBuilder;
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







    public interface IDynamicMiddleware
    {
        void Register(IApplicationBuilder applicationBuilder);
    }

    public interface IResourceMiddleware : IDynamicMiddleware
    {
    }

    public interface ILogicMiddleware : IDynamicMiddleware
    {
    }

    public class TestResourceMiddleware : IResourceMiddleware
    {
        public void Register(IApplicationBuilder appBuilder)
        {
            appBuilder.Map("/test", newAppBuilder => newAppBuilder.Run(async context => await context.Response.WriteAsync("Agent!")));
        }
    }
    public class HeaderLogicMiddleware : ILogicMiddleware
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