using System;
using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Web
{
    public class DefaultRequestRuntimeProvider : IRequestRuntimeProvider
    {
        private readonly ITypeService _typeService;

        public DefaultRequestRuntimeProvider(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public IEnumerable<IRequestRuntime> Runtimes
        {
            get { return _typeService.Resolve<IRequestRuntime>().ToArray(); }
        }
    }
}