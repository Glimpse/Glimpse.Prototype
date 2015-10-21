using System;
using System.Collections.Generic;
using System.Linq;
using Glimpse.Agent.Internal.Inspectors.EF.Proxies;
using Glimpse.Agent.Messages;
using Microsoft.Extensions.DiagnosticAdapter;

namespace Glimpse.Agent.Internal.Inspectors.Mvc
{
    public partial class WebDiagnosticsInspector
    {
        [DiagnosticName("Microsoft.Data.Entity.BeforeExecuteCommand")]
        public void OnBeforeExecuteCommand(IDbCommand command, string executeMethod, bool isAsync)
        {
            var startDateTime = DateTime.UtcNow;

            var message = new BeforeExecuteCommandMessage
            {
                CommandMethod = executeMethod,
                CommandIsAsync = isAsync,
                CommandText = command.CommandText,
                CommandType = command.CommandType,
                //CommandParameters = command.Parameters,
                CommandStartTime = startDateTime
            };

            _broker.BeginLogicalOperation(message, startDateTime);
            _broker.SendMessage(message);
        }

        [DiagnosticName("Microsoft.Data.Entity.AfterExecuteCommand")]
        public void OnAfterExecuteCommand(IDbCommand command, string executeMethod, bool isAsync)
        {
            var timing = _broker.EndLogicalOperation<BeforeExecuteCommandMessage>().Timing;

            var message = new AfterExecuteCommandMessage
            {
                CommandHadException = false,
                CommandDuration = timing.Elapsed,
                CommandEndTime = timing.End
            };

            _broker.SendMessage(message);
        }

        [DiagnosticName("Microsoft.Data.Entity.CommandExecutionError")]
        public void OnAfterExecuteCommand(IDbCommand command, string executeMethod, bool isAsync, Exception exception)
        {
            var timing = _broker.EndLogicalOperation<BeforeExecuteCommandMessage>().Timing;

            var message = new AfterExecuteCommandExceptionMessage
            {
                //Exception = exception,
                CommandHadException = true,
                CommandDuration = timing.Elapsed,
                CommandEndTime = timing.End
            };

            _broker.SendMessage(message);

        }
    }
}
