
namespace Glimpse.Agent
{
    public static class AgentBrokerExtensions
    {
        public static void BeginLogicalOperation(this IAgentBroker broker, object message)
        {
            broker.Context.Operations.PushOperation(message);
            broker.SendMessage(message);
        }

        public static OperationTiming<T> EndLogicalOperation<T>(this IAgentBroker broker)
        {
            var timing = broker.Context.Operations.PopOperation<T>();
            return timing;
        }
    }
}
