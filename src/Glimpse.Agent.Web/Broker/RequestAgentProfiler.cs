using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace Glimpse.Agent.Web
{
    public class RequestAgentProfiler : IMiddlewareProfilerComposer
    {
        private readonly IAgentBroker _messageBus;

        public RequestAgentProfiler(IAgentBroker messageBus)
        {
            _messageBus = messageBus;
        }
        
        public void Register(IApplicationBuilder appBuilder)
        {
            appBuilder.Use(async (context, next) =>
            {
                var beginMessage = new BeginRequestMessage(context.Request);
                _messageBus.BeginLogicalOperation(beginMessage);

                await next();

                var timing = _messageBus.EndLogicalOperation<BeginRequestMessage>().Timing;
                var endMessage = new EndRequestMessage(context.Request, timing); 
                _messageBus.SendMessage(endMessage);
            });
        }
    }
}