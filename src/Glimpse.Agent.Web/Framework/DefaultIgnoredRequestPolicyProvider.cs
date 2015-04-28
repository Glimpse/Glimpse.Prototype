using System;
using System.Collections.Generic;
using System.Linq; 

namespace Glimpse.Agent.Web
{
    public class DefaultIgnoredRequestPolicyProvider : IIgnoredRequestPolicyProvider
    {
        private readonly ITypeService _typeService;

        public DefaultIgnoredRequestPolicyProvider(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public IEnumerable<IRequestIgnorePolicy> Policies
        {
            get { return _typeService.Resolve<IRequestIgnorePolicy>().ToArray(); }
        }
    }
}