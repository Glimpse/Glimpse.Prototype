using System;
using System.Collections.Generic;
using System.Linq; 

namespace Glimpse.Agent.Web
{
    public class DefaultInspectorStartupProvider : IInspectorStartupProvider
    {
        private readonly ITypeService _typeService;

        public DefaultInspectorStartupProvider(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public IEnumerable<IInspectorStartup> Startups
        {
            get { return _typeService.Resolve<IInspectorStartup>().ToArray(); }
        }
    }
}