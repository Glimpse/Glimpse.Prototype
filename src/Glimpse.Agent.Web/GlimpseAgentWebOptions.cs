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
            IgnoredUris = new List<Regex>();
            StatusCodes = new List<int>();
            ContentTypes = new List<string>();
        }

        public IList<Regex> IgnoredUris { get; }

        public IList<int> StatusCodes { get; }

        public IList<string> ContentTypes { get; }
    }
}