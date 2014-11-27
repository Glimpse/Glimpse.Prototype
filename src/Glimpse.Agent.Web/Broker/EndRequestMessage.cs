using System;
using Glimpse;

namespace Glimpse.Agent.Web
{
    public class EndRequestMessage : BaseMessage
    {
        public EndRequestMessage(Guid requestId)
        {
            RequestId = requestId;
        }

        public Guid RequestId { get; }
    }
}