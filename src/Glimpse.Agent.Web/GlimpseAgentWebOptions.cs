using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Glimpse.Agent.Web.Options;

namespace Glimpse.Agent.Web
{
    public class GlimpseAgentWebOptions
    {
        public GlimpseAgentWebOptions()
        {
            IgnoredUris = new List<IgnoredUrisDescriptor>();
            StatusCodes = new List<IgnoredUrisDescriptor>();
        }

        public IList<IgnoredUrisDescriptor> IgnoredUris { get; }

        public IList<IgnoredUrisDescriptor> StatusCodes { get; }
    }
}