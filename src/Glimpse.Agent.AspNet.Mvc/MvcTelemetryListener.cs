using System.Collections.Generic;
using System.Linq;
using Glimpse.Agent.AspNet.Mvc.Messages;
using Glimpse.Agent.AspNet.Mvc.Proxies;
using Microsoft.Framework.TelemetryAdapter;

namespace Glimpse.Agent.AspNet.Mvc
{
    public class MvcTelemetryListener
    {
        private readonly IAgentBroker _broker;
        private readonly ProxyAdapter _proxyAdapter;

        public MvcTelemetryListener(IAgentBroker broker)
        {
            _broker = broker;

            _proxyAdapter = new ProxyAdapter();
            _proxyAdapter.Register("Microsoft.AspNet.Mvc.ViewResult");
            _proxyAdapter.Register("Microsoft.AspNet.Mvc.ContentResult");
            _proxyAdapter.Register("Microsoft.AspNet.Mvc.ObjectResult");
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

        // Note: This event is the start of the action execution. The action has been selected, the route
        //       has been selected, filters have run and model binding has occured.
        [TelemetryName("Microsoft.AspNet.Mvc.BeforeActionMethod")]
        public void OnBeforeActionMethod(
            IActionContext actionContext,
            IDictionary<string, object> actionArguments)
        {
            var actionDescriptor = actionContext.ActionDescriptor;

            var message = new BeforeActionInvokedMessage
            {
                ActionId = actionDescriptor.Id,
                DisplayName = actionDescriptor.DisplayName,
                ActionName = actionDescriptor.Name,
                ControllerName = actionDescriptor.ControllerName,
                Binding = actionArguments.Select(x => new BindingData { Type = x.Value?.GetType(), Name = x.Key, Value = x.Value }).ToList()
            };

            _broker.BeginLogicalOperation(message);
        }

        [TelemetryName("Microsoft.AspNet.Mvc.AfterActionMethod")]
        public void OnAfterActionMethod(
            IActionContext actionContext,
            IActionResult result)
        {
            var timing = _broker.EndLogicalOperation<BeforeActionInvokedMessage>().Timing;

            var message = new AfterActionInvokedMessage()
            {
                ActionId = actionContext.ActionDescriptor.Id,
                Timing = timing
            };

            _broker.SendMessage(message);
        }
        
        // Note: This event is the start of the result pipeline. The action has been executed, but
        //       we haven't yet determined which view (if any) will handle the request
        [TelemetryName("Microsoft.AspNet.Mvc.BeforeActionResult")]
        public void OnBeforeActionResult(
            IActionContext actionContext,
            object result)
        {
            var actionDescriptor = actionContext.ActionDescriptor;

            var message = new BeforeActionResultMessage
            {
                ActionId = actionDescriptor.Id,
                DisplayName = actionDescriptor.DisplayName,
                ActionName = actionDescriptor.Name,
                ControllerName = actionDescriptor.ControllerName
            };

            // TODO: Need to work off the inheritence chain 
            //var inheritancHierarchy = result.GetType().GetInheritancHierarchy().ToList();

            var actionResult = new ActionResultData();
            switch (result.GetType().FullName)
            {
                case "Microsoft.AspNet.Mvc.ViewResult":
                    var viewResult = _proxyAdapter.Process<ActionResult.IViewResult>("Microsoft.AspNet.Mvc.ViewResult", result);

                    actionResult.Type = "ViewResult";
                    actionResult.Data = new ActionResultData.ViewResult
                    {
                        ViewName = viewResult.ViewName,
                        StatusCode = viewResult.StatusCode,
                        TempData = viewResult.TempData,
                        ViewData = viewResult.ViewData,
                        ContentType = viewResult.ContentType?.ToString()
                    };

                    break;
                case "Microsoft.AspNet.Mvc.ContentResult":
                    var contentResult = _proxyAdapter.Process<ActionResult.IContentResult>("Microsoft.AspNet.Mvc.ContentResult", result);

                    actionResult.Type = "ContentResult";
                    actionResult.Data = new ActionResultData.ContentResult
                    {
                        StatusCode = contentResult.StatusCode,
                        Content = contentResult.Content,
                        ContentType = contentResult.ContentType?.ToString()
                    };

                    break;
                case "Microsoft.AspNet.Mvc.ObjectResult":
                    var objectResult = _proxyAdapter.Process<ActionResult.IObjectResult>("Microsoft.AspNet.Mvc.ContentResult", result);

                    actionResult.Type = "ContentResult";
                    actionResult.Data = new ActionResultData.ObjectResult
                    {
                        StatusCode = objectResult.StatusCode,
                        Value = objectResult.Value,
                        Formatters = objectResult.Formatters?.Select(x => x.GetType()).ToList(),
                        ContentTypes = objectResult.ContentTypes?.Select(x => x.ToString()).ToList()
                    };

                    break;
            }
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
