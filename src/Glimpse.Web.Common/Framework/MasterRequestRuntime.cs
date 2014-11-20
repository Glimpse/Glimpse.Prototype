using System;
using System.Collections.Generic;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse.Web
{
    public class MasterRequestRuntime
    {
        private readonly IEnumerable<IRequestRuntime> _requestRuntimes;

        public MasterRequestRuntime(IServiceProvider serviceProvider)
        {
            _requestRuntimes = serviceProvider.GetService<IDiscoverableCollection<IRequestRuntime>>();
        }

        public void Begin(IContext newContext)
        {

        }

        public void End(IContext newContext)
        {

        }
    }
}