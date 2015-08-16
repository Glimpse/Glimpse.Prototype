using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Server.Web
{
    public class DefaultRequestAuthorizerProvider : IRequestAuthorizerProvider
    {
        private readonly ITypeService _typeService;

        public DefaultRequestAuthorizerProvider(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public IEnumerable<IRequestAuthorizer> Authorizers
        {
            get { return _typeService.Resolve<IRequestAuthorizer>().ToArray(); }
        }
    }
}