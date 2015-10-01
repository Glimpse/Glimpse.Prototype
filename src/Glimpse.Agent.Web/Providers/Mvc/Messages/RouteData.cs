
using System.Collections.Generic;

namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public class RouteData
    {
        public string Name { get; set; }

        public string Pattern { get; set; }

        public IList<KeyValuePair<string, string>> Data { get; set; }

        public IList<KeyValuePair<string, RouteConfigurationData>> Configuration { get; set; }
    }

    public class RouteConfigurationData
    {
        public string Default { get; set; }

        public bool Optional { get; set; }
    }
}
