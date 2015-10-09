using System;
using Glimpse.Internal;

namespace Glimpse.Agent
{
    public static class AgentBrokerExtensions
    {
        public static void BeginLogicalOperation(this IAgentBroker broker, object message)
        {
            // TODO: Don't really like that I'm doing it this way
            var operations = new OperationStack();
            operations.PushOperation(message);
        }

        public static void BeginLogicalOperation(this IAgentBroker broker, object message, DateTime dateTime)
        {
            var operations = new OperationStack();
            operations.PushOperation(new Operation(message, dateTime));
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
