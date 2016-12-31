using System;
using System.Linq;
using Glimpse.Agent.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DiagnosticAdapter;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http.Internal;
using System.IO;
using System.Text;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Agent.Internal.Inspectors
{
    public partial class WebDiagnosticsInspector
    {
        private static int MaxBodySize = 132000;

        [DiagnosticName("Microsoft.AspNetCore.Hosting.BeginRequest")]
        public void OnBeginRequest(HttpContext httpContext)
        {
            // TODO: Not sure if this is where this should live but it's the earlist hook point we have
            _contextData.Value = new MessageContext { Id = Guid.NewGuid(), Type = "Request" };

            var request = httpContext.Request;
            var requestHeaders = request.GetTypedHeaders();
            var requestDateTime = DateTime.UtcNow;

            // set things up so we can read the body
            request.EnableRewind();

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
            message.Protocol = new WebRequestProtocol();
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
            message.Body = new WebRequestBody();
            if (request.HasFormContentType)
            {
                message.Body.Form = request.Form;
            }
            ReadBody(message.Body, requestHeaders.ContentType, request.Body);

            _broker.StartOffsetOperation();
            _broker.BeginLogicalOperation(message, requestDateTime);
            _broker.SendMessage(message);
        }

        [DiagnosticName("Microsoft.AspNetCore.Hosting.EndRequest")]
        public void OnEndRequest(HttpContext httpContext)
        {
            var message = new WebResponseMessage();
            ProcessEnd(message, httpContext);

            _broker.SendMessage(message);
        }
        
        [DiagnosticName("Microsoft.AspNetCore.Hosting.UnhandledException")]
        public void OnHostingUnhandledException(HttpContext httpContext, Exception exception)
        {
            var message = new WebResponseExceptionMessage();
            ProcessEnd(message, httpContext);
            ProcessException(message, exception, false);

            _broker.SendMessage(message);
        }

        [DiagnosticName("Microsoft.AspNetCore.Diagnostics.UnhandledException")]
        public void OnDiagnosticsUnhandledException(HttpContext httpContext, Exception exception)
        {
            var message = new WebResponseExceptionMessage();
            ProcessEnd(message, httpContext);
            ProcessException(message, exception, false);

            _broker.SendMessage(message);
        }

        [DiagnosticName("Microsoft.AspNetCore.Diagnostics.HandledException")]
        public void OnDiagnosticsHandledException(HttpContext httpContext, Exception exception)
        {
            var message = new WebResponseExceptionMessage();
            ProcessEnd(message, httpContext);
            ProcessException(message, exception, true);

            _broker.SendMessage(message);
        }
        
        private void ProcessEnd(WebResponseMessage message, HttpContext httpContext)
        {
            var request = httpContext.Request;
            var response = httpContext.Response;
            var responseHeaders = response.GetTypedHeaders();

            // build message
            message.Url = $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}{request.QueryString}"; // TODO: check if there is a better way of doing this
            message.Headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
            message.StatusCode = response.StatusCode;

            // add body
            message.Body = new WebBody();
            ReadBody(message.Body, responseHeaders.ContentType, response.Body);

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

        private void ReadBody(WebBody webBody, MediaTypeHeaderValue contentType, Stream body)
        {
            if (contentType != null)
            {
                webBody.Encoding = contentType.Encoding.ToString();
  
                if (webBody.Encoding == "UTF8")
                {
                    // read content
                    using (var reader = new StreamReader(body, Encoding.UTF8, true, 1024, true))
                    {
                        webBody.Content = reader.ReadToEnd();
                    }

                    // set position back to 0 so others can read
                    body.Position = 0;

                    // trim if needed
                    if (!string.IsNullOrEmpty(webBody.Content) && webBody.Content.Length > MaxBodySize)
                    {
                        webBody.Content = webBody.Content.Substring(0, MaxBodySize);
                        webBody.IsTruncated = true;
                    }

                    // recird size
                    webBody.Size = webBody.Content != null ? webBody.Content.Length : 0;
                }
            }
        }
    }
}
