using System;
using Glimpse;
using Glimpse.Web;

namespace Glimpse.Agent.Web
{
    public class EndRequestMessage : BaseMessage
    {
        public EndRequestMessage(Guid requestId, IHttpRequest request)
        {
            RequestId = requestId;
            Uri = request.Uri();
        }

        public Guid RequestId { get; }

        public string Uri { get; }
    }
}