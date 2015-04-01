using System;
using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Web
{
    public class DefaultRequestHandlerProvider : IRequestHandlerProvider
    {
        private readonly ITypeService _typeService;

        public DefaultRequestHandlerProvider(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public IEnumerable<IRequestHandler> Handlers
        {
            get { return _typeService.Resolve<IRequestHandler>().ToArray(); }
        }
    }
}