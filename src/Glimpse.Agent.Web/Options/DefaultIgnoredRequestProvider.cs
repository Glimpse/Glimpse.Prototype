using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace Glimpse.Agent.Web.Options
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