using System;
using Microsoft.AspNet.Http;
using System.Collections.Generic;
using Microsoft.AspNet.Builder;

namespace Glimpse.Web
{
    public class RequestRuntimeHost
    {
        private readonly IEnumerable<IRequestAuthorizer> _requestAuthorizers;

        public RequestRuntimeHost(IRequestAuthorizerProvider requestAuthorizerProvider)
        {
            _requestAuthorizers = requestAuthorizerProvider.Authorizers; 
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
    }





    /*
     
    public class TestResourceMiddlewareComposer : IMiddlewareResourceComposer
    {
        public void Register(IApplicationBuilder appBuilder)
        {
            appBuilder.Map("/test", newAppBuilder => newAppBuilder.Run(async context => await context.Response.WriteAsync("Agent!")));
        }
    }
    public class HeaderLogicMiddlewareComposer : IMiddlewareLogicComposer
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
    */
}