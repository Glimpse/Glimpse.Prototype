using Microsoft.Framework.Logging;
using System;

namespace Glimpse.Agent
{
    public class DefaultLogger : ILogger
    {
        private IMessageAgentBus _messageBus;

        public DefaultLogger(IMessageAgentBus messageBus)
        {
            _messageBus = messageBus;
        }

        public IDisposable BeginScope(object state)
        {
            return null;
        }

        public bool IsEnabled(TraceType eventType)
        {
            return true;
        }

        public void Write(TraceType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
        }
    }
}