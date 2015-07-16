
using System.Linq;
using Glimpse.Agent.AspNet.Mvc.Messages;
using Glimpse.Agent.AspNet.Mvc.Proxies;
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

            var message = new ActionSelectedMessage()
            {
                ActionId = actionDescriptor.Id,
                DisplayName = actionDescriptor.DisplayName,
                RouteData = new RouteData()
                {
                    Name = realRouter.Name,
                    Pattern = realRouter.RouteTemplate,
                    Data = routeData.Values.Select(kvp => new RouteResolutionData()
                    {
                        Tag = kvp.Key,
                        Match = kvp.Value.ToString()
                    }).ToList(),
                }
            };


            _broker.BeginLogicalOperation(message);
        }

        [NotificationName("Microsoft.AspNet.Mvc.ActionInvoked")]
        public void OnActionInvoked(
            IActionDescriptor actionDescriptor,
            IHttpContext httpContext)
        {
            var timing = _broker.EndLogicalOperation<ActionSelectedMessage>().Timing;
            var message = new ActionInvokedMessage()
            {
                ActionId = actionDescriptor.Id,
                DisplayName = actionDescriptor.DisplayName,
                Timing = timing
            };

            _broker.SendMessage(message);
        }
    }
}
