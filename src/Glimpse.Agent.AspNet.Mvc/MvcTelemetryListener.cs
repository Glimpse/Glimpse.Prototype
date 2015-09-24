using System.Collections.Generic;
using System.Linq;
using Glimpse.Agent.AspNet.Mvc.Messages;
using Glimpse.Agent.AspNet.Mvc.Proxies;
using Microsoft.AspNet.Routing;
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
            var router = routeData.Routers[routeData.Routers.Count - 2];

            var message = new BeforeActionMessage
            {
                ActionId = actionDescriptor.Id,
                DisplayName = actionDescriptor.DisplayName,
                ActionName = actionDescriptor.Name,
                ControllerName = actionDescriptor.ControllerName,
                RouteData = new Glimpse.Agent.AspNet.Mvc.Messages.RouteData()
                {
                    Name = router.Name,
                    Pattern = router.RouteTemplate
                }
            };

            // I don't think there is a case where one can exist without the other
            if (routeData.Values != null && router.ParsedTemplate != null)
            {
                // going through and merging the two lists of data... neither list contains 
                // everything that we need.
                message.RouteData.Data = routeData.Values.GroupJoin(router.ParsedTemplate,
                    c => c.Key,
                    b => b.Name,
                    (c, b) => {
                        var result = new RouteResolutionData()
                        {
                            Tag = c.Key,
                            Match = c.Value.ToString()
                        };

                        var template = b.FirstOrDefault();
                        if (template != null)
                        {
                            result.Default = template.DefaultValue.ToString();
                            result.Optional = template.IsOptional;
                        }

                        return result;
                    }).ToList();
            }

            _broker.BeginLogicalOperation(message);
        }

        [TelemetryName("Microsoft.AspNet.Mvc.AfterAction")]
        public void OnAfterAction(IActionDescriptor actionDescriptor, IHttpContext httpContext)
        {
            var timing = _broker.EndLogicalOperation<BeforeActionMessage>().Timing;

            var message = new AfterActionMessage()
            {
                ActionId = actionDescriptor.Id,
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
            var message = new ViewFoundMessage()
            {
                ActionId = actionContext.ActionDescriptor.Id,
                Name = viewName,
            };
        }
    }

    public class ViewFoundMessage
    {
        public string ActionId { get; set; }

        public string Name { get; set; }
    }
}
