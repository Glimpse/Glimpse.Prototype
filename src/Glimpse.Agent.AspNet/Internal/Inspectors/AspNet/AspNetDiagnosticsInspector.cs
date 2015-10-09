using System;
using System.Collections.Generic;
using System.Linq;
using Glimpse.Agent.Messages;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.TelemetryAdapter;

namespace Glimpse.Agent.Internal.Inspectors.Mvc
{
    public partial class WebDiagnosticsInspector
    {
        [TelemetryName("Microsoft.AspNet.Hosting.BeginRequest")]
        public void OnBeginRequest(HttpContext httpContext)
        {
            // TODO: Not sure if this is where this should live but it's the earlist hook point we have
            _contextData.Value = new MessageContext { Id = Guid.NewGuid(), Type = "Request" };

            var request = httpContext.Request;
            var requestDateTime = DateTime.UtcNow;

            var message = new BeginRequestMessage
            {
                // TODO: check if there is a better way of doing this
                RequestUrl = $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}{request.QueryString}",
                RequestPath = request.Path,
                RequestQueryString = request.QueryString.Value,
                RequestMethod = request.Method,
                RequestHeaders = request.Headers,
                RequestContentLength = request.ContentLength,
                RequestContentType = request.ContentType,
                RequestStartTime = requestDateTime
            };
            
            _broker.BeginLogicalOperation(message, requestDateTime);
            _broker.SendMessage(message);
        }

        [TelemetryName("Microsoft.AspNet.Hosting.EndRequest")]
        public void OnEndRequest(HttpContext httpContext)
        {
            var timing = _broker.EndLogicalOperation<BeginRequestMessage>().Timing;

            var request = httpContext.Request;
            var response = httpContext.Response;

            var message = new EndRequestMessage
            {
                // TODO: check if there is a better way of doing this
                RequestUrl = $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}{request.QueryString}",
                RequestPath = request.Path,
                RequestQueryString = request.QueryString.Value,
                ResponseDuration = Math.Round(timing.Elapsed.TotalMilliseconds, 2),
                ResponseHeaders = response.Headers,
                ResponseContentLength = response.ContentLength,
                ResponseContentType = response.ContentType,
                ResponseStatusCode = response.StatusCode,
                ResponseEndTime = timing.End.ToUniversalTime()
            };
            
            _broker.SendMessage(message);
        }
    }
}
