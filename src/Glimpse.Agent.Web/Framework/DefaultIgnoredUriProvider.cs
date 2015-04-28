using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Framework.OptionsModel;

namespace Glimpse.Agent.Web
{
    public class DefaultIgnoredUriProvider : IIgnoredUriProvider
    { 
        public DefaultIgnoredUriProvider(IOptions<GlimpseAgentWebOptions> optionsAccessor)
        {
            var ignoredUris = optionsAccessor.Options.IgnoredUris;
            IgnoredUris = ignoredUris.ToList(); 
        }

        public IReadOnlyList<Regex> IgnoredUris { get; }
    }
}