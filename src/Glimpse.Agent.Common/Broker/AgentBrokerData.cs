using System;

namespace Glimpse.Agent
{
    public class AgentBrokerData
    {
        public AgentBrokerData(object payload, MessageContext context)
        {
            Payload = payload;
            Context = context;
        }

        public object Payload { get; set; }

        public MessageContext Context { get; set; }
    }
}