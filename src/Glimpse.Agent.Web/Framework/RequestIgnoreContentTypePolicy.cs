using System.Collections.Generic;
using System.Linq;
using Glimpse.Agent.Web;
using Glimpse.Web;
using Microsoft.Framework.OptionsModel;

namespace Glimpse.Agent.Web.Framework
{
    public class RequestIgnoreContentTypePolicy : IRequestIgnorePolicy
    {
        private readonly IReadOnlyCollection<string> _contextType;

        public RequestIgnoreContentTypePolicy(IIgnoredContentTypeProvider ignoredContentTypeProvider)
        {
            _contextType = ignoredContentTypeProvider.ContentTypes;
        }

        public bool ShouldIgnore(IHttpContext context)
        {
            return _contextType.Contains(context.Response.ContentType);
        }
    }
}