namespace Glimpse.Agent.Web
{
    public class ProfilerInspectorStartup : IInspectorStartup
    {
        private readonly IAgentBroker _messageBus;

        public ProfilerInspectorStartup(IAgentBroker messageBus)
        {
            _messageBus = messageBus;
        }
        
        public void Configure(IInspectorBuilder inspectorBuilder)
        {
            inspectorBuilder.Use(async (context, next) =>
            {
                var beginMessage = new BeginRequestMessage(context.Request);
                _messageBus.BeginLogicalOperation(beginMessage);

                await next();

                var timing = _messageBus.EndLogicalOperation<BeginRequestMessage>().Timing;
                var endMessage = new EndRequestMessage(context.Request, context.Response, timing);
                _messageBus.SendMessage(endMessage);
            });
        }
    }
}