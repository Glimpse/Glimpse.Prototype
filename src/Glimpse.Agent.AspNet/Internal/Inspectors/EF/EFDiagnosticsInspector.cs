using System;
using System.Collections.Generic;
using System.Linq;
using Glimpse.Agent.Internal.Inspectors.EF.Proxies;
using Glimpse.Agent.Messages;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.TelemetryAdapter;

namespace Glimpse.Agent.Internal.Inspectors.Mvc
{
    public partial class WebDiagnosticsInspector
    {
        [TelemetryName("Microsoft.Data.Entity.BeforeExecuteCommand")]
        public void OnBeforeExecuteCommand(IDbCommand command, string executeMethod, bool isAsync)
        {
            var beginMessage = new BeforeExecuteCommandMessage
            {
                CommandMethod = executeMethod,
                CommandIsAsync = isAsync
            };

            _broker.BeginLogicalOperation(beginMessage);
        }

        [TelemetryName("Microsoft.Data.Entity.AfterExecuteCommand")]
        public void OnAfterExecuteCommand(IDbCommand command, string executeMethod, bool isAsync)
        {
            var timing = _broker.EndLogicalOperation<BeforeExecuteCommandMessage>().Timing;

            var endMessage = new AfterExecuteCommandMessage
            {
                CommandMethod = executeMethod,
                CommandIsAsync = isAsync,
                CommandText = command.CommandText,
                CommandType = command.CommandType,
                CommandParameters = null,
                CommandDuration = timing.Elapsed,
                CommandStartTime = timing.Start,
                CommandEndTime = timing.End,
                CommandHadException = false
            };

            _broker.SendMessage(endMessage);
        }

        [TelemetryName("Microsoft.Data.Entity.CommandExecutionError")]
        public void OnAfterExecuteCommand(IDbCommand command, string executeMethod, bool isAsync, Exception exception)
        {
            var timing = _broker.EndLogicalOperation<BeforeExecuteCommandMessage>().Timing;
            var endMessage = new AfterExecuteCommandMessage
            {
                CommandMethod = executeMethod,
                CommandIsAsync = isAsync,
                CommandText = command.CommandText,
                CommandType = command.CommandType,
                CommandParameters = null,
                CommandDuration = timing.Elapsed,
                CommandStartTime = timing.Start,
                CommandEndTime = timing.End,
                CommandHadException = false
            };

            _broker.SendMessage(endMessage);

        }
    }
}
