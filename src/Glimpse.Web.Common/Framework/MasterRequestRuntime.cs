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

        public MasterRequestRuntime(IServiceProvider serviceProvider)
        {
            _requestRuntimes = serviceProvider.GetService<IDiscoverableCollection<IRequestRuntime>>();
            _requestRuntimes.Discover();

            _requestHandlers = serviceProvider.GetService<IDiscoverableCollection<IRequestHandler>>();
            _requestHandlers.Discover();
        }

        public async Task Begin(IContext context)
        {
            foreach (var requestRuntime in _requestRuntimes)
            {
                await requestRuntime.Begin(context);
            }
        }


        public bool TryGetHandle(IContext context, out IRequestHandler handeler)
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

        public async Task End(IContext context)
        {
            foreach (var requestRuntime in _requestRuntimes)
            {
                await requestRuntime.End(context);
            }
        }
    }
}