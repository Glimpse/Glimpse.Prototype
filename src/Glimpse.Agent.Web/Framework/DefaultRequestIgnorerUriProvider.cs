using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Framework.OptionsModel;

namespace Glimpse.Agent.Web
{
    public class DefaultRequestIgnorerUriProvider : IRequestIgnorerUriProvider
    { 
        public DefaultRequestIgnorerUriProvider(IOptions<GlimpseAgentWebOptions> optionsAccessor)
        {
            var ignoredUris = optionsAccessor.Value.IgnoredUris;
            IgnoredUris = ignoredUris.ToList(); 
        }

        public IReadOnlyList<Regex> IgnoredUris { get; }
    }
}