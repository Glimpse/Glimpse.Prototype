using System;
using System.Collections.Generic;
using Microsoft.Framework.DependencyInjection;
using System.Threading.Tasks;

namespace Glimpse.Web
{
    public class RequestRuntimeHost
    {
        private readonly IEnumerable<IRequestRuntime> _requestRuntimes;
        private readonly IEnumerable<IRequestHandler> _requestHandlers;
        private readonly IEnumerable<IRequestAuthorizer> _requestAuthorizers;

        public RequestRuntimeHost(IRequestAuthorizerProvider requestAuthorizerProvider, IRequestRuntimeProvider requestRuntimesProvider, IRequestHandlerProvider requestHandlersProvider)
        {
            _requestAuthorizers = requestAuthorizerProvider.Authorizers; 
            _requestRuntimes = requestRuntimesProvider.Runtimes; 
            _requestHandlers = requestHandlersProvider.Handlers; 
        }

        public bool Authorized(IHttpContext context)
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

        public async Task Begin(IHttpContext context)
        {
            foreach (var requestRuntime in _requestRuntimes)
            {
                await requestRuntime.Begin(context);
            }
        }

        public bool TryGetHandle(IHttpContext context, out IRequestHandler handeler)
        {
            foreach (var requestHandler in _requestHandlers)
            {
                if (requestHandler.WillHandle(context))
                {
                    handeler = requestHandler;
                    return true;
                }
            }

            handeler = null;
            return false;
        }

        public async Task End(IHttpContext context)
        {
            foreach (var requestRuntime in _requestRuntimes)
            {
                await requestRuntime.End(context);
            }
        }
    }
}