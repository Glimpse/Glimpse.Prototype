using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Server.Web
{
    public class DefaultMiddlewareResourceComposerProvider : IMiddlewareResourceComposerProvider
    {
        private readonly ITypeService _typeService;

        public DefaultMiddlewareResourceComposerProvider(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public IEnumerable<IMiddlewareResourceComposer> Resources
        {
            get { return _typeService.Resolve<IMiddlewareResourceComposer>().ToArray(); }
        }
    }
}