using System;
using Glimpse.Agent.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DiagnosticAdapter;
using System.Diagnostics;

namespace Glimpse.Agent.Internal.Inspectors
{
    public partial class WebDiagnosticsInspector
    {
        [DiagnosticName("Microsoft.AspNetCore.MiddlewareAnalysis.MiddlewareStarting")]
        public void OnMiddlewareFinished(
            string name,
            HttpContext httpContext,
            Guid instanceId
            )
        {
            var message = new MiddlewareStartMessage();
            message.Name = name;
            message.CorrelationId = instanceId;
        }

        [DiagnosticName("Microsoft.AspNetCore.MiddlewareAnalysis.MiddlewareFinished")]
        public void OnMiddlewareFinished(
            string name,
            Guid instanceId,
            long duration,
            HttpContext httpContext)
        {
            var message = new MiddlewareEndMessage();
            ProcessEnd(message, name, instanceId, duration, httpContext);

            _broker.SendMessage(message);
        }

        [DiagnosticName("Microsoft.AspNetCore.MiddlewareAnalysis.MiddlewareException")]
        public void OnMiddlewareException(
            string name,
            Guid instanceId,
            long duration,
            HttpContext httpContext,
            Exception exception)
        {
            var message = new MiddlewareEndExceptionMessage();
            ProcessEnd(message, name, instanceId, duration, httpContext);
            ProcessException(message, exception, false);

            _broker.SendMessage(message);
        }

        private void ProcessEnd(
            MiddlewareEndMessage message,
            string name,
            Guid instanceId,
            long duration,
            HttpContext httpContext)
        {
            message.CorrelationId = instanceId;
            message.Duration = (duration / Stopwatch.Frequency) * 1000;
        }

        private void ProcessException(IExceptionMessage message, Exception exception)
        {
            // store the BaseException as the exception of record 
            var baseException = exception.GetBaseException();
            message.ExceptionTypeName = baseException.GetType().Name;
            message.ExceptionMessage = baseException.Message;
            message.ExceptionDetails = _exceptionProcessor.GetErrorDetails(exception);
        }
    }
}
