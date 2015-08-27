using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Agent.Web
{
    public class DefaultInspectorProvider : IInspectorProvider
    {
        private readonly ITypeService _typeService;

        public DefaultInspectorProvider(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public IEnumerable<IInspector> Inspectors
        {
            get { return _typeService.Resolve<IInspector>().ToArray(); }
        }
    }
}