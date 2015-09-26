using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.Framework.TelemetryAdapter;

namespace Glimpse.Agent.Web
{
    public class WebTelemetryListener
    {

        [TelemetryName("Microsoft.AspNet.Hosting.BeginRequest")]
        public void OnBeginRequest(HttpContext httpContext)
        {

        }

        [TelemetryName("Microsoft.AspNet.Hosting.EndRequest")]
        public void OnEndRequest(HttpContext httpContext)
        {

        }
    }
}
