using System.Collections.Generic;

namespace Glimpse.Agent.Messages
{
    public interface IActionRouteMessage
    {
        string ActionId { get; set; }
        
        string RouteName { get; set; }

        string RoutePattern { get; set; }

        IList<KeyValuePair<string, string>> RouteData { get; set; }

        IList<KeyValuePair<string, RouteConfigurationData>> RouteConfiguration { get; set; }
    }
}