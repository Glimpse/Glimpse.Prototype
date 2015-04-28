using System;
using System.Collections.Generic;
using System.Linq; 

namespace Glimpse.Agent.Web
{
    public class DefaultRequestIgnorerProvider : IRequestIgnorerProvider
    {
        private readonly ITypeService _typeService;

        public DefaultRequestIgnorerProvider(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public IEnumerable<IRequestIgnorer> Policies
        {
            get { return _typeService.Resolve<IRequestIgnorer>().ToArray(); }
        }
    }
}