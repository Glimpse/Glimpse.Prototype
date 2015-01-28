using Microsoft.Framework.Logging;
using System;

namespace Glimpse.Agent
{
    public class DefaultLogger : ILogger
    {
        private IAgentBroker _messageBus;

        public DefaultLogger(IAgentBroker messageBus)
        {
            _messageBus = messageBus;
        }

        public IDisposable BeginScope(object state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel eventType)
        {
            return true;
        }

        public void Write(LogLevel eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
        }
    }
}