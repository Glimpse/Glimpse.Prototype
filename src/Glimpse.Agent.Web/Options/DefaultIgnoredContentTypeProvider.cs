using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Framework.OptionsModel;

namespace Glimpse.Agent.Web.Options
{ 
    public class DefaultIgnoredContentTypeProvider : IIgnoredContentTypeProvider
    {
        public DefaultIgnoredContentTypeProvider(IOptions<GlimpseAgentWebOptions> optionsAccessor)
        {
            var contentTypes = optionsAccessor.Options.ContentTypes;
            ContentTypes = contentTypes.ToList();
        }

        public IReadOnlyList<IgnoredContentTypeDescriptor> ContentTypes { get; }
    }
}