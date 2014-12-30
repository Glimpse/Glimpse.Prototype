using Microsoft.Framework.Logging;
using System;

namespace Glimpse.Agent
{

    public class DefaultLoggerProvider : ILoggerProvider
    {
        private readonly IMessageAgentBus _messageBus;

        public DefaultLoggerProvider(IMessageAgentBus messageBus)
        {
            _messageBus = messageBus;
        }

        public ILogger Create(string name)
        { 
            return new DefaultLogger(_messageBus);
        }
    }
}