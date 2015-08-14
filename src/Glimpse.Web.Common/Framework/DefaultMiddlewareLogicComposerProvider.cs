using System;
using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Web
{
    public class DefaultMiddlewareLogicComposerProvider : IMiddlewareLogicComposerProvider
    {
        private readonly ITypeService _typeService;

        public DefaultMiddlewareLogicComposerProvider(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public IEnumerable<IMiddlewareLogicComposer> Logic
        {
            get { return _typeService.Resolve<IMiddlewareLogicComposer>().ToArray(); }
        }
    }
}