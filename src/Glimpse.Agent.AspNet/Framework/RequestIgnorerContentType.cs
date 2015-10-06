using Microsoft.AspNet.Http;
using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Agent.Framework
{
    public class RequestIgnorerContentType : IRequestIgnorer
    {
        private readonly IReadOnlyCollection<string> _contextType;

        public RequestIgnorerContentType(IRequestIgnorerContentTypeProvider requestIgnorerContentTypeProvider)
        {
            _contextType = requestIgnorerContentTypeProvider.ContentTypes;
        }

        public bool ShouldIgnore(HttpContext context)
        {
            return _contextType.Contains(context.Response.ContentType);
        }
    }
}