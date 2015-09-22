
using System.Collections.Generic;
using System.Linq;
using Glimpse.Agent.AspNet.Mvc.Messages;
using Glimpse.Agent.AspNet.Mvc.Proxies;
using Microsoft.AspNet.Routing.Template;
using Microsoft.Framework.TelemetryAdapter;

namespace Glimpse.Agent.AspNet.Mvc
{
    public class MvcTelemetryListener
    {
        private readonly IAgentBroker _broker;

        public MvcTelemetryListener(IAgentBroker broker)
        {
            _broker = broker;
        }

        // Note: This event is the start of the action pipeline. The action has been selected, the route
        //       has been selected but no filters have run and model binding hasn't occured.
        [TelemetryName("Microsoft.AspNet.Mvc.BeforeAction")]
        public void OnBeforeAction(IActionDescriptor actionDescriptor, IHttpContext httpContext, IRouteData routeData)
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

        [TelemetryName("Microsoft.AspNet.Mvc.AfterAction")]
        public void OnAfterAction(
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

        [TelemetryName("Microsoft.AspNet.Mvc.BeforeActionMethod")]
        public void OnBeforeActionMethod(
            IActionContext actionContext,
            IDictionary<string, object> actionArguments)
        {
        }

        [TelemetryName("Microsoft.AspNet.Mvc.AfterActionMethod")]
        public void OnAfterActionMethod(
            IActionContext actionContext,
            IActionResult result)
        {
        }

        [TelemetryName("Microsoft.AspNet.Mvc.BeforeActionResult")]
        public void OnBeforeActionResult(
            IActionContext actionContext,
            IActionResult result)
        {
        }

        [TelemetryName("Microsoft.AspNet.Mvc.AfterActionResult")]
        public void OnAfterActionResult(
            IActionContext actionContext,
            IActionResult result)
        {
        }

        [TelemetryName("Microsoft.AspNet.Mvc.ViewResultViewNotFound")]
        public void OnViewResultViewNotFound(
            IActionContext actionContext,
            IActionResult result,
            string viewName,
            IReadOnlyList<string> searchedLocations)
        {
        }

        [TelemetryName("Microsoft.AspNet.Mvc.ViewResultViewFound")]
        public void OnViewResultViewFound(
            IActionContext actionContext,
            IActionResult result,
            string viewName,
            IView view)
        {
        }
    }
}
