using Glimpse.Web;
using Microsoft.Framework.Logging;
using System;
using System.Threading.Tasks;

namespace Glimpse.Agent.Web
{
    public class AgentRuntime : IRequestRuntime
    {
        private readonly string _requestIdKey = "RequestId";
        private readonly IMessageAgentBus _messageBus;

        public AgentRuntime(IMessageAgentBus messageBus, ILoggerFactory loggingFactory)
        {
            _messageBus = messageBus;

            //// TODO: This is a REALLY bad place for this, not sure where else to put it
            //loggingFactory.AddProvider(new DefaultLoggerProvider(messageBus));
            //var test = loggingFactory.Create("test");
            //test.Write(TraceType.Information, 123, new { Test = "test" }, null, (x, y) => { return ""; });
            //// TODO: This is a REALLY bad place for this, not sure where else to put it
        }

        public async Task Begin(IContext newContext)
        {
            var requestId = Guid.NewGuid();

            newContext.Items.Add(_requestIdKey, requestId);

            var message = new BeginRequestMessage(requestId, newContext.Request);

            // TODO: Full out message more

            await _messageBus.SendMessage(message);
        }

        public async Task End(IContext newContext)
        {
            var requestId = (Guid)newContext.Items[_requestIdKey];

            var message = new EndRequestMessage(requestId, newContext.Request);

            // TODO: Full out message more

            await _messageBus.SendMessage(message);
        }
    }
}