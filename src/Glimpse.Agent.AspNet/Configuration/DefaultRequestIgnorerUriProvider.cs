using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.OptionsModel;

namespace Glimpse.Agent.Configuration
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