using System;
using Glimpse;
using Glimpse.Web;

namespace Glimpse.Agent.Web
{
    public class BeginRequestMessage : BaseMessage
    {
        public BeginRequestMessage(Guid requestId, IHttpRequest request)
        {
            RequestId = requestId;
            Uri = request.Uri();
        }

        public Guid RequestId { get; }

        public string Uri { get; }
    }
}