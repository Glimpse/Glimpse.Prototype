using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.OptionsModel;

namespace Glimpse.Agent.Web
{ 
    public class DefaultRequestIgnorerContentTypeProvider : IRequestIgnorerContentTypeProvider
    {
        public DefaultRequestIgnorerContentTypeProvider(IOptions<GlimpseAgentWebOptions> optionsAccessor)
        {
            var contentTypes = optionsAccessor.Value.IgnoredContentTypes;
            ContentTypes = contentTypes.ToList();
        }

        public IReadOnlyList<string> ContentTypes { get; }
    }
}