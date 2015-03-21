using System.Collections.Generic;
using Glimpse.Web;
using Microsoft.Framework.OptionsModel;

namespace Glimpse.Agent.Web.Framework
{
    public class ContentTypeIgnoredRequestPolicy : IIgnoredRequestPolicy
    {
        private readonly IList<string> _contextType;

        public ContentTypeIgnoredRequestPolicy(IOptions<GlimpseAgentWebOptions> optionsAccessor)
        {
            _contextType = optionsAccessor.Options.IgnoredContentTypes;
        }

        public bool ShouldIgnore(IHttpContext context)
        {
            return _contextType.Contains(context.Response.ContentType);
        }
    }
}