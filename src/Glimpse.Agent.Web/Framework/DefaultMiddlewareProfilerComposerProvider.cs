using System;
using System.Collections.Generic;
using System.Linq; 

namespace Glimpse.Agent.Web
{
    public class DefaultMiddlewareProfilerComposerProvider : IMiddlewareProfilerComposerProvider
    {
        private readonly ITypeService _typeService;

        public DefaultMiddlewareProfilerComposerProvider(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public IEnumerable<IMiddlewareProfilerComposer> Profilers
        {
            get { return _typeService.Resolve<IMiddlewareProfilerComposer>().ToArray(); }
        }
    }
}