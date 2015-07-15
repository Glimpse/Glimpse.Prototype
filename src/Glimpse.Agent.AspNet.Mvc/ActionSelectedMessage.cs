using System;
using System.Collections.Generic;

namespace Glimpse.Agent.AspNet.Mvc
{
    internal class ActionSelectedMessage : IMessage
    {
        public Guid Id { get; } = Guid.NewGuid();

        public DateTime Time { get; } = DateTime.Now;

        public string ActionId { get; set; }

        public string DisplayName { get; set; }

        public RouteData RouteData { get; set; }
    }

    internal class RouteData
    {
        public string Name { get; set; }
        public string Pattern { get; set; }
        public IList<RouteResolutionData> Data { get; set; }
    }

    internal class RouteResolutionData
    {
        public string Tag { get; set; }

        public string Match { get; set; }
    }
}