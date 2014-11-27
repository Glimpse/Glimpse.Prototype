using System;
using Glimpse;

namespace Glimpse.Agent.Web
{
    public class BeginRequestMessage : BaseMessage
    {
        public BeginRequestMessage(Guid requestId)
        {
            RequestId = requestId;
        }

        public Guid RequestId { get; }
    }
}