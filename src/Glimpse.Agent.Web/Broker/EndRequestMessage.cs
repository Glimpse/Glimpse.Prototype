using System;
using System.Collections.Generic;
using Glimpse;
using Glimpse.Web;

namespace Glimpse.Agent.Web
{
    public class EndRequestMessage : IMessageTag
    {
        public EndRequestMessage(IHttpRequest request, Timing timing)
        {
            Uri = request.Uri();
        }

        public Timing Timing { get; }

        public string Uri { get; }

        public IEnumerable<string> Tags
        {
            get { return new List<string> { "Test" }; }
        }
    }
}