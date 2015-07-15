using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Routing.Template;
using Microsoft.Framework.Notification;

namespace Glimpse.Agent.AspNet.Mvc
{
    public class MvcNotificationListener
    {
        private readonly IAgentBroker _broker;

        public MvcNotificationListener(IAgentBroker broker)
        {
            _broker = broker;
        }

        [NotificationName("Microsoft.AspNet.Mvc.ActionSelected")]
        public void OnActionSelected(
            IActionDescriptor actionDescriptor,
            IHttpContext httpContext,
            IRouteData routeData)
        {
            var realRouter = routeData.Routers[routeData.Routers.Count - 2] as TemplateRoute;


            _broker.SendMessage(new ActionSelectedMessage()
            {
                DisplayName = actionDescriptor.DisplayName,
                ActionId = actionDescriptor.Id,
                RouteData = new RouteData()
                {
                    Name = realRouter.Name,
                    Pattern = realRouter.RouteTemplate,
                    Data = routeData.Values.Select(kvp => new RouteResolutionData() { Tag = kvp.Key, Match = kvp.Value.ToString() }).ToList(),
                }
            });
        }
    }

    public interface IActionDescriptor
    {
        string Id { get; }
        string DisplayName { get; }
    }

    public interface IHttpContext
    {

    }

    public interface IRouteData
    {
        IReadOnlyList<object> Routers { get; }
        IDictionary<string, object> DataTokens { get; }
        IDictionary<string, object> Values { get; }
    }

    //public interface IRouter
    //{
    //    string Name { get; }

    //    string Template { get; }
    //}
}
