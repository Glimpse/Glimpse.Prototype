using System;
using System.Collections.Generic;
using Glimpse;
using Glimpse.Web;

namespace Glimpse.Agent.Web
{
    public class EndRequestMessage : BaseMessage, IMessageTag
    {
        public EndRequestMessage(IHttpRequest request)
        {
            Uri = request.Uri();
        }

        public string Uri { get; }

        public IEnumerable<string> Tags
        {
            get { return new List<string> { "Test" }; }
        }
    }
}