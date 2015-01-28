using Glimpse.Web;
using Microsoft.Framework.Logging;
using System;
using System.Threading.Tasks;

namespace Glimpse.Agent.Web
{
    public class AgentProfiler : IRequestProfiler
    {
        private readonly string _requestIdKey = "RequestId";
        private readonly IAgentBroker _messageBus;

        public AgentProfiler(IAgentBroker messageBus, ILoggerFactory loggingFactory)
        {
            _messageBus = messageBus;

            //// TODO: This is a REALLY bad place for this, not sure where else to put it
            //loggingFactory.AddProvider(new DefaultLoggerProvider(messageBus));
            //var test = loggingFactory.Create("test");
            //test.Write(LogLevel.Information, 123, new { Test = "test" }, null, (x, y) => { return ""; });
            //// TODO: This is a REALLY bad place for this, not sure where else to put it
        }

        public async Task Begin(IHttpContext newContext)
        { 
            var message = new BeginRequestMessage(newContext.Request);

            // TODO: Full out message more

            await _messageBus.SendMessage(message);
        }

        public async Task End(IHttpContext newContext)
        { 
            var message = new EndRequestMessage(newContext.Request);

            // TODO: Full out message more

            await _messageBus.SendMessage(message);
        }
    }
}