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
            StatusCodes = new List<IgnoredStatusCodeDescriptor>();
            ContentTypes = new List<IgnoredContentTypeDescriptor>();
        }

        public IList<IgnoredUrisDescriptor> IgnoredUris { get; }

        public IList<IgnoredStatusCodeDescriptor> StatusCodes { get; }

        public IList<IgnoredContentTypeDescriptor> ContentTypes { get; }
    }
}