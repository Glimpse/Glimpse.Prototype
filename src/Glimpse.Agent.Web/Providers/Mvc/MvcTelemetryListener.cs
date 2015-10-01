using System.Collections.Generic;
using System.Linq;
using Glimpse.Agent.AspNet.Mvc.Messages;
using Glimpse.Agent.AspNet.Mvc.Proxies;
using Microsoft.Framework.TelemetryAdapter;

namespace Glimpse.Agent.Web
{
    public partial class WebTelemetryListener
    {
        partial void MvcOnCreated()
        {
            _proxyAdapter.Register("Microsoft.AspNet.Mvc.ViewResult");
            _proxyAdapter.Register("Microsoft.AspNet.Mvc.ContentResult");
            _proxyAdapter.Register("Microsoft.AspNet.Mvc.ObjectResult");
            _proxyAdapter.Register("Microsoft.AspNet.Routing.Template.TemplateRoute");
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
                RouteData = new RouteData
                {
                    Data = routeData.Values?.Select(x => new KeyValuePair<string, string>(x.Key, x.Value?.ToString())).ToList()
                }
            };

            if (router.GetType().FullName == "Microsoft.AspNet.Routing.Template.TemplateRoute")
            {
                var templateRoute = _proxyAdapter.Process<IRouter>("Microsoft.AspNet.Routing.Template.TemplateRoute", router);

                var messageRouteData = message.RouteData;
                messageRouteData.Name = templateRoute.Name;
                messageRouteData.Pattern = templateRoute.RouteTemplate;
                messageRouteData.Configuration = templateRoute.ParsedTemplate?.Parameters?.Select(x => {
                        var config = new RouteConfigurationData { Default = x.DefaultValue?.ToString(), Optional = x.IsOptional };
                        return new KeyValuePair<string, RouteConfigurationData>(x.Name, config);

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
                Binding = actionArguments?.Select(x => new BindingData { Type = x.Value?.GetType(), Name = x.Key, Value = x.Value }).ToList()
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

            // TODO: currently looking to see if this switch code and ProxyAdapter can be
            //       consumed by Microsoft.Framework.TelemetryAdapter
            var actionResult = new ActionResultData();
            switch (result.GetType().FullName)
            {
                case "Microsoft.AspNet.Mvc.ViewResult":
                    var viewResult = _proxyAdapter.Process<ActionResultTypes.IViewResult>("Microsoft.AspNet.Mvc.ViewResult", result);

                    actionResult.Type = "ViewResult";
                    actionResult.Data = new ActionResultData.ViewResult
                    {
                        ViewName = viewResult.ViewName,
                        StatusCode = viewResult.StatusCode,
                        //TempData = viewResult.TempData,  //Not including here atm... being captured by ViewResultViewFound/ViewResultViewNotFound instead
                        //ViewData = viewResult.ViewData,
                        ContentType = viewResult.ContentType?.ToString()
                    };

                    break;
                case "Microsoft.AspNet.Mvc.ContentResult":
                    var contentResult = _proxyAdapter.Process<ActionResultTypes.IContentResult>("Microsoft.AspNet.Mvc.ContentResult", result);

                    actionResult.Type = "ContentResult";
                    actionResult.Data = new ActionResultData.ContentResult
                    {
                        StatusCode = contentResult.StatusCode,
                        Content = contentResult.Content,
                        ContentType = contentResult.ContentType?.ToString()
                    };

                    break;
                case "Microsoft.AspNet.Mvc.ObjectResult":
                    var objectResult = _proxyAdapter.Process<ActionResultTypes.IObjectResult>("Microsoft.AspNet.Mvc.ContentResult", result);

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

            // only link up the details if we processed the actionresult
            if (!string.IsNullOrEmpty(actionResult.Type))
            {
                message.ActionResult = actionResult;
            }

            _broker.BeginLogicalOperation(message);
        }

        [TelemetryName("Microsoft.AspNet.Mvc.AfterActionResult")]
        public void OnAfterActionResult(
            IActionContext actionContext,
            IActionResult result)
        {
            var timing = _broker.EndLogicalOperation<BeforeActionResultMessage>().Timing;

            var message = new AfterActionResultMessage()
            {
                ActionId = actionContext.ActionDescriptor.Id,
                Timing = timing
            };

            _broker.SendMessage(message);
        }

        // NOTE: This event is only fired when we dont find any matches at all. This executes
        //       at the end of the matching process. You will never get a ViewResultViewNotFound 
        //       and ViewResultViewFound event firing for the same view resolution.
        [TelemetryName("Microsoft.AspNet.Mvc.ViewResultViewNotFound")]
        public void OnViewResultViewNotFound(
            IActionContext actionContext,
            ActionResultTypes.IViewResult result,
            string viewName,
            IReadOnlyList<string> searchedLocations)
        {
            var message = new ViewResultFoundStatusMessage()
            {
                ActionId = actionContext.ActionDescriptor.Id,
                ActionName = actionContext.ActionDescriptor.Name,
                ControllerName = actionContext.ActionDescriptor.ControllerName,
                ViewName = viewName,
                DidFind = true,
                SearchedLocations = searchedLocations,
                Path = null,
                ViewData = new ViewResult
                {
                    ViewData = result.ViewData,
                    TempData = result.TempData
                }
            };

            _broker.SendMessage(message);
        }

        // NOTE: This event is only fired when we do find a match. This executes at the end of
        //       the matching process. You will never get a ViewResultViewNotFound and 
        //       ViewResultViewFound event firing for the same view resolution.
        [TelemetryName("Microsoft.AspNet.Mvc.ViewResultViewFound")]
        public void OnViewResultViewFound(
            IActionContext actionContext,
            ActionResultTypes.IViewResult result,
            string viewName,
            IView view)
        {
            var message = new ViewResultFoundStatusMessage()
            {
                ActionId = actionContext.ActionDescriptor.Id,
                ActionName = actionContext.ActionDescriptor.Name,
                ControllerName = actionContext.ActionDescriptor.ControllerName,
                ViewName = viewName,
                DidFind = true,
                SearchedLocations = null, // Don't have this yet :(
                Path = view.Path,
                ViewData = new ViewResult {
                    ViewData = result.ViewData,
                    TempData = result.TempData
                }
            };

            _broker.SendMessage(message);
        }
    }
}
