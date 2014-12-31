using System;
using Glimpse;
using Glimpse.Web;

namespace Glimpse.Agent.Web
{
    public class EndRequestMessage : BaseMessage
    {
        public EndRequestMessage(IHttpRequest request)
        {
            Uri = request.Uri();
        }

        public string Uri { get; }
    }
}