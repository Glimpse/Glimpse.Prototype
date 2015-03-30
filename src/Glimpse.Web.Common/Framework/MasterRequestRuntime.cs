using System;
using System.Collections.Generic;
using Microsoft.Framework.DependencyInjection;
using System.Threading.Tasks;

namespace Glimpse.Web
{
    public class MasterRequestRuntime
    {
        private readonly IDiscoverableCollection<IRequestRuntime> _requestRuntimes;
        private readonly IDiscoverableCollection<IRequestHandler> _requestHandlers;
        private readonly IDiscoverableCollection<IRequestAuthorizer> _requestAuthorizers;

        public MasterRequestRuntime(IDiscoverableCollection<IRequestAuthorizer> requestAuthorizers, IDiscoverableCollection<IRequestRuntime> requestRuntimes, IDiscoverableCollection<IRequestHandler> requestHandlers)
        {
            _requestAuthorizers = requestAuthorizers;
            _requestAuthorizers.Discover();

            _requestRuntimes = requestRuntimes;
            _requestRuntimes.Discover();

            _requestHandlers = requestHandlers;
            _requestHandlers.Discover();
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