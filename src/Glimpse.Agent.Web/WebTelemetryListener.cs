using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glimpse.Agent.Web.Messages;
using Microsoft.AspNet.Http;
using Microsoft.Framework.TelemetryAdapter;

namespace Glimpse.Agent.Web
{
    public class WebTelemetryListener
    {
        private readonly IAgentBroker _broker;

        public WebTelemetryListener(IAgentBroker broker)
        {
            _broker = broker;
        }

        [TelemetryName("Microsoft.AspNet.Hosting.BeginRequest")]
        public void OnBeginRequest(HttpContext httpContext)
        {
            //var request = httpContext.Request;

            //var beginMessage = new BeginRequestMessage
            //{
            //    // TODO: check if there is a better way of doing this
            //    // TODO: should there be a StartTime property here?
            //    Url = $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}{request.QueryString}"
            //};

            //_broker.BeginLogicalOperation(beginMessage);
        }

        [TelemetryName("Microsoft.AspNet.Hosting.EndRequest")]
        public void OnEndRequest(HttpContext httpContext)
        {
            //var timing = _broker.EndLogicalOperation<BeginRequestMessage>().Timing;

            //var request = httpContext.Request;
            //var response = httpContext.Response;

            //var endMessage = new EndRequestMessage
            //{
            //    // TODO: check if there is a better way of doing this
            //    Url = $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}{request.QueryString}",
            //    Duration = timing.Elapsed.TotalMilliseconds,
            //    Method = request.Method,
            //    ContentType = response.ContentType,
            //    StatusCode = response.StatusCode, 
            //    StartTime = timing.Start.ToUniversalTime(),
            //    EndTime = timing.End.ToUniversalTime()
            //};

            //_broker.SendMessage(endMessage);
        }
    }
}
