
using System.Collections.Generic;

namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public class RouteData
    {
        public string Name { get; set; }

        public string Pattern { get; set; }

        public IList<RouteResolutionData> Data { get; set; }
    }
}
