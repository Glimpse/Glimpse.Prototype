using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Server.Web
{
    public class DefaultResourceProvider : IResourceProvider
    {
        private readonly ITypeService _typeService;

        public DefaultResourceProvider(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public IEnumerable<IResource> Resources
        {
            get { return _typeService.Resolve<IResource>().ToArray(); }
        }
    }
}