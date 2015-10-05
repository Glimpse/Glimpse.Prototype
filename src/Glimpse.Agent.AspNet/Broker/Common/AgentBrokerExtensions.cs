
namespace Glimpse.Agent
{
    public static class AgentBrokerExtensions
    {
        public static void BeginLogicalOperation(this IAgentBroker broker, object message)
        {
            // TODO: Don't really like that I'm doing it this way
            var operations = new OperationStack();
            operations.PushOperation(message);
            broker.SendMessage(message);
        }

        public static OperationTiming<T> EndLogicalOperation<T>(this IAgentBroker broker)
        {
            // TODO: Don't really like that I'm doing it this way
            var operations = new OperationStack();
            var timing = operations.PopOperation<T>();
            return timing;
        }
    }
}
