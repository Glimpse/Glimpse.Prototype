using System;
using Glimpse;
using Glimpse.Web;

namespace Glimpse.Agent.Web
{
    public class BeginRequestMessage : BaseMessage
    {
        public BeginRequestMessage(IHttpRequest request)
        {
            Uri = request.Uri();
        }

        public string Uri { get; }
    }
}