using System;
using System.Collections.Generic;
using System.Linq; 

namespace Glimpse.Agent.Web
{
    public class DefaultRequestProfilerProvider : IRequestProfilerProvider
    {
        private readonly ITypeService _typeService;

        public DefaultRequestProfilerProvider(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public IEnumerable<IRequestProfiler> Profilers
        {
            get { return _typeService.Resolve<IRequestProfiler>().ToArray(); }
        }
    }
}