namespace Glimpse.Agent
{
    public class AgentBrokerPayload
    {
        public AgentBrokerPayload(object payload, MessageContext context)
        {
            Payload = payload;
            Context = context;
        }

        public object Payload { get; set; }

        public MessageContext Context { get; set; }
    }
}