using System;
using System.Threading.Tasks;

namespace Glimpse.Agent
{
    public interface IAgentBroker
    {
        /// <summary>
        /// On the sender thread and is blocking
        /// </summary>
        AgentBrokerOptions OnSenderThread { get; }

        /// <summary>
        /// Off the sender thread and is not blocking
        /// </summary>
        AgentBrokerOptions OffSenderThread { get; }

        void SendMessage(object message);
    }
}
