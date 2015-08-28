using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Server.Web
{
    public class DefaultResourceStartupProvider : IResourceStartupProvider
    {
        private readonly ITypeService _typeService;

        public DefaultResourceStartupProvider(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public IEnumerable<IResourceStartup> Startups
        {
            get { return _typeService.Resolve<IResourceStartup>().ToArray(); }
        }
    }
}