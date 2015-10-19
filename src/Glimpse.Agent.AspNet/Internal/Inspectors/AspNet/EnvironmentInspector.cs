using System;
using Glimpse.Agent.AspNet.Messages;
using Glimpse.Agent.Inspectors;
using Microsoft.AspNet.Http;

namespace Glimpse.Agent.AspNet.Internal.Inspectors.AspNet
{
    public class EnvironmentInspector : Inspector
    {
        private readonly IAgentBroker _broker;
        private EnvironmentMessage _message;

        public EnvironmentInspector(IAgentBroker broker)
        {
            _broker = broker;
        }

        public override void Before(HttpContext context)
        {
            if (_message == null)
            {
                _message = new EnvironmentMessage
                {
                    Server = Environment.MachineName,
                    OperatingSystem = Environment.OSVersion.VersionString,
                    ProcessorCount = Environment.ProcessorCount,
                    Is64Bit = Environment.Is64BitOperatingSystem,
                    CommandLineArgs = Environment.GetCommandLineArgs(),
                    EnvironmentVariables = Environment.GetEnvironmentVariables()
                };
            }

            _broker.SendMessage(_message);
        }
    }
}
