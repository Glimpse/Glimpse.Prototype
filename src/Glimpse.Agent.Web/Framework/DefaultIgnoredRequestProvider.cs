using System;
using System.Collections.Generic;
using System.Linq; 

namespace Glimpse.Agent.Web
{
    public class DefaultIgnoredRequestProvider : IIgnoredRequestProvider
    {
        private readonly ITypeService _typeService;

        public DefaultIgnoredRequestProvider(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public IEnumerable<IIgnoredRequestPolicy> Policies
        {
            get { return _typeService.Resolve<IIgnoredRequestPolicy>().ToArray(); }
        }
    }
}