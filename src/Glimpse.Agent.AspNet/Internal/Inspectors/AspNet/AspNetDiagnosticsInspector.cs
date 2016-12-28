using System;
using System.Linq;
using Glimpse.Agent.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DiagnosticAdapter;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Glimpse.Agent.Internal.Inspectors
{
    public partial class WebDiagnosticsInspector
    {
        [DiagnosticName("Microsoft.AspNetCore.Hosting.BeginRequest")]
        public void OnBeginRequest(HttpContext httpContext)
        {
            // TODO: Not sure if this is where this should live but it's the earlist hook point we have
            _contextData.Value = new MessageContext { Id = Guid.NewGuid(), Type = "Request" };

            var request = httpContext.Request;
            var requestHeaders = request.GetTypedHeaders();
            var requestDateTime = DateTime.UtcNow;
            
            // build message
            var message = new WebRequestMessage
            {
                Url = $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}{request.QueryString}", // TODO: check if there is a better way of doing this
                Method = request.Method,
                Headers = request.Headers.ToDictionary(h => h.Key, h => h.Value),
                StartTime = requestDateTime
            };
            
            // add ajax
            var isAjax = StringValues.Empty;
            httpContext.Request.Headers.TryGetValue("__glimpse-isAjax", out isAjax);
            message.IsAjax = isAjax == "true";

            // add protocol
            message.Protocol = new RequestProtocol();
            if (!string.IsNullOrEmpty(request.Protocol))
            {
                var protocol = request.Protocol.Split('/');
                message.Protocol.Identifier = protocol[0];
                if (protocol.Length > 0)
                {
                    message.Protocol.Version = protocol[1];
                }
            }

            // add body
            message.Body = new RequestBody
            {
                IsTruncated = false
                //Content = request.Body, // TODO: need to read this safely
            };
            if (request.HasFormContentType)
            {
                message.Body.Form = request.Form;
            }
            var contentType = requestHeaders.ContentType;
            if (contentType != null)
            {
                message.Body.Encoding = contentType.Encoding.ToString();
            }

            _broker.StartOffsetOperation();
            _broker.BeginLogicalOperation(message, requestDateTime);
            _broker.SendMessage(message);
        }

        [DiagnosticName("Microsoft.AspNetCore.Hosting.EndRequest")]
        public void OnEndRequest(HttpContext httpContext)
        {
            var message = new WebResponseMessage();
            ProcessEndRequest(message, httpContext);

            _broker.SendMessage(message);
        }
        
        [DiagnosticName("Microsoft.AspNetCore.Hosting.UnhandledException")]
        public void OnHostingUnhandledException(HttpContext httpContext, Exception exception)
        {
            var message = new HostingExceptionMessage();
            ProcessEndRequest(message, httpContext);
            ProcessException(message, exception, false);

            _broker.SendMessage(message);
        }

        [DiagnosticName("Microsoft.AspNetCore.Diagnostics.UnhandledException")]
        public void OnDiagnosticsUnhandledException(HttpContext httpContext, Exception exception)
        {
            var message = new DiagnosticsExceptionMessage();
            ProcessException(message, exception, false);

            _broker.SendMessage(message);
        }

        [DiagnosticName("Microsoft.AspNetCore.Diagnostics.HandledException")]
        public void OnDiagnosticsHandledException(HttpContext httpContext, Exception exception)
        {
            var message = new DiagnosticsExceptionMessage();
            ProcessException(message, exception, true);

            _broker.SendMessage(message);
        }
        
        private void ProcessEndRequest(WebResponseMessage message, HttpContext httpContext)
        {
            var request = httpContext.Request;
            var response = httpContext.Response;
            var responseHeaders = response.GetTypedHeaders();

            // build message
            message.Url = $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}{request.QueryString}"; // TODO: check if there is a better way of doing this
            message.Headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
            message.StatusCode = response.StatusCode;

            // add body
            message.Body = new ResponseBody
            {
                IsTruncated = false
                //Content = request.Body, // TODO: need to read this safely
            };
            var contentType = responseHeaders.ContentType;
            if (contentType != null)
            {
                message.Body.Encoding = contentType.Encoding.ToString();
            }

            // add timing data
            var timing = _broker.EndLogicalOperation<WebRequestMessage>();
            if (timing != null)
            {
                message.Duration = Math.Round(timing.Elapsed.TotalMilliseconds, 2);
                message.EndTime = timing.End.ToUniversalTime();
            }
            else
            {
                // in this case still want to publish but setting duration to 0
                message.Duration = 0.0;
                message.EndTime = DateTime.UtcNow;

                _logger.LogCritical("ProcessEndRequest: Still published `WebResponseMessage` but couldn't find `BeginRequestMessage` in stack");
            }
        }

        private void ProcessException(IExceptionMessage message, Exception exception, bool isHandelled)
        {
            // store the BaseException as the exception of record 
            var baseException = exception.GetBaseException();
            message.ExceptionIsHandelled = isHandelled;
            message.ExceptionTypeName = baseException.GetType().Name;
            message.ExceptionMessage = baseException.Message;
            message.ExceptionDetails = _exceptionProcessor.GetErrorDetails(exception);
        }
    }
}
