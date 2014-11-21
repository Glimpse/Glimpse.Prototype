using Glimpse.Web;
using System;

namespace Glimpse.Agent.Web
{
    public class AgentRuntime : IRequestRuntime
    {
        public void Begin(IContext newContext)
        {
            var message = new BeginRequestMessage();

            // TODO: Publish messages 
        }

        public void End(IContext newContext)
        {
            var message = new EndRequestMessage();

            // TODO: Publish messages 
        }
    }
}