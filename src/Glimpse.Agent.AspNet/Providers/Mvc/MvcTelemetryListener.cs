using System.Collections.Generic;
using System.Linq;
using Glimpse.Agent.AspNet.Mvc.Messages;
using Glimpse.Agent.AspNet.Mvc.Proxies;
using Glimpse.Internal;
using Microsoft.Extensions.TelemetryAdapter;

namespace Glimpse.Agent.AspNet
{
    public partial class WebTelemetryListener
    {
        partial void MvcOnCreated()
        {
            _proxyAdapter.Register("Microsoft.AspNet.Mvc.ViewResult");
            _proxyAdapter.Register("Microsoft.AspNet.Mvc.ContentResult");
            _proxyAdapter.Register("Microsoft.AspNet.Mvc.ObjectResult");
            _proxyAdapter.Register("Microsoft.AspNet.Routing.Template.TemplateRoute");
            _proxyAdapter.Register("Microsoft.AspNet.Mvc.Controllers.ControllerActionDescriptor");
            _proxyAdapter.Register("Microsoft.AspNet.Mvc.Abstractions.ActionDescriptor");
        }

        // NOTE: This event is the start of the action pipeline. The action has been selected, the route
        //       has been selected but no filters have run and model binding hasn't occured.
        [TelemetryName("Microsoft.AspNet.Mvc.BeforeAction")]
        public void OnBeforeAction(object actionDescriptor, IHttpContext httpContext, IRouteData routeData)
        {
            var typedActionDescriptor = ConvertActionDescriptor(actionDescriptor);

            var message = new BeforeActionMessage
            {
                ActionId = typedActionDescriptor.Id,
                DisplayName = typedActionDescriptor.DisplayName,
                ActionName = typedActionDescriptor.Name,
                ControllerName = typedActionDescriptor.ControllerName,
                RouteData = routeData.Values?.Select(x => new KeyValuePair<string, string>(x.Key, x.Value?.ToString())).ToList()
            };

            // NOTE: Template data is only available in the TemplateRoute, so we need to try and 
            //       promote that type into something we can use
            var router = routeData.Routers[routeData.Routers.Count - 2];
            if (router.GetType().FullName == "Microsoft.AspNet.Routing.Template.TemplateRoute")
            {
                var templateRoute = _proxyAdapter.Process<IRouter>("Microsoft.AspNet.Routing.Template.TemplateRoute", router);
                
                message.RouteName = templateRoute.Name;
                message.RoutePattern = templateRoute.RouteTemplate;
                message.RouteConfiguration = templateRoute.ParsedTemplate?.Parameters?.Select(x => {
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

        // NOTE: This event is the start of the action execution. The action has been selected, the route
        //       has been selected, filters have run and model binding has occured.
        [TelemetryName("Microsoft.AspNet.Mvc.BeforeActionMethod")]
        public void OnBeforeActionMethod(
            IActionContext actionContext,
            IDictionary<string, object> arguments)
        {
            var actionDescriptor = ConvertActionDescriptor(actionContext.ActionDescriptor);

            var message = new BeforeActionInvokedMessage
            {
                ActionId = actionDescriptor.Id,
                DisplayName = actionDescriptor.DisplayName,
                ActionName = actionDescriptor.Name,
                ControllerName = actionDescriptor.ControllerName,
                Binding = arguments?.Select(x => new BindingData { Type = x.Value?.GetType(), Name = x.Key, Value = x.Value }).ToList()
            };

            _broker.BeginLogicalOperation(message);
        }

        [TelemetryName("Microsoft.AspNet.Mvc.AfterActionMethod")]
        public void OnAfterActionMethod(
            IActionContext actionContext,
            IActionResult result)
        {
            var timing = _broker.EndLogicalOperation<BeforeActionInvokedMessage>().Timing;
            var actionDescriptor = ConvertActionDescriptor(actionContext.ActionDescriptor);

            var message = new AfterActionInvokedMessage()
            {
                ActionId = actionDescriptor.Id,
                DisplayName = actionDescriptor.DisplayName,
                ActionName = actionDescriptor.Name,
                ControllerName = actionDescriptor.ControllerName,
                TargetClass = actionDescriptor.ControllerTypeInfo.Name,
                TargetMethod = actionDescriptor.MethodInfo.Name,
                Timing = timing
            };

            _broker.SendMessage(message);
        }

        // NOTE: This event is the start of the result pipeline. The action has been executed, but
        //       we haven't yet determined which view (if any) will handle the request
        [TelemetryName("Microsoft.AspNet.Mvc.BeforeActionResult")]
        public void OnBeforeActionResult(
            IActionContext actionContext,
            object result)
        {
            var actionDescriptor = ConvertActionDescriptor(actionContext.ActionDescriptor);

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
            //       consumed by Microsoft.Extensions.TelemetryAdapter
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
            var actionDescriptor = ConvertActionDescriptor(actionContext.ActionDescriptor);

            var message = new AfterActionResultMessage()
            {
                ActionId = actionDescriptor.Id,
                Timing = timing
            };

            _broker.SendMessage(message);
        }

        // NOTE: This event is only fired when we dont find any matches at all. This executes
        //       at the end of the matching process. You will never get a ViewResultViewNotFound 
        //       and ViewResultViewFound event firing for the same view resolution.
        [TelemetryName("Microsoft.AspNet.Mvc.ViewNotFound")]
        public void OnViewResultViewNotFound(
            IActionContext actionContext,
            ActionResultTypes.IViewResult result,
            string viewName,
            IReadOnlyList<string> searchedLocations)
        {
            var actionDescriptor = ConvertActionDescriptor(actionContext.ActionDescriptor);

            var message = new ViewResultFoundStatusMessage()
            {
                ActionId = actionDescriptor.Id,
                ActionName = actionDescriptor.Name,
                ControllerName = actionDescriptor.ControllerName,
                ViewName = viewName,
                DidFind = true,
                SearchedLocations = searchedLocations,
                Path = null,
                ViewData = new ViewResult
                {
                    ViewData = result.ViewData,
                    TempData = result.TempData
                },
                Timing = new Timing() // TODO: to be removed
            };

            _broker.SendMessage(message);
        }

        // NOTE: This event is only fired when we do find a match. This executes at the end of
        //       the matching process. You will never get a ViewResultViewNotFound and 
        //       ViewResultViewFound event firing for the same view resolution.
        [TelemetryName("Microsoft.AspNet.Mvc.ViewFound")]
        public void OnViewResultViewFound(
            IActionContext actionContext,
            ActionResultTypes.IViewResult result,
            string viewName,
            IView view)
        {
            var actionDescriptor = ConvertActionDescriptor(actionContext.ActionDescriptor);

            var message = new ViewResultFoundStatusMessage()
            {
                ActionId = actionDescriptor.Id,
                ActionName = actionDescriptor.Name,
                ControllerName = actionDescriptor.ControllerName,
                ViewName = viewName,
                DidFind = true,
                SearchedLocations = null, // Don't have this yet :(
                Path = view.Path,
                //ViewData = new ViewResult {      // TODO: because we switch threads, we need to make sure we get
                //    ViewData = result.ViewData,  //       what we need off the thread before publishing
                //    TempData = result.TempData
                //},
                Timing = new Timing() // TODO: to be removed
            };

            _broker.SendMessage(message);
        }

        private IActionDescriptor ConvertActionDescriptor(object actionDescriptor)
        {
            var typedActionDescriptor = (IActionDescriptor)null;

            // NOTE: ActionDescriptor is usually ControllerActionDescriptor but the compile time type is
            //       ActionDescriptor. This is a problem because we are misisng the ControllerName which 
            //       we use a lot.
            switch (actionDescriptor.GetType().FullName)
            {
                case "Microsoft.AspNet.Mvc.Controllers.ControllerActionDescriptor":
                    typedActionDescriptor = _proxyAdapter.Process<IActionDescriptor>("Microsoft.AspNet.Mvc.Controllers.ControllerActionDescriptor", actionDescriptor);
                    break;
                case "Microsoft.AspNet.Mvc.Abstractions.ActionDescriptor":
                    typedActionDescriptor = _proxyAdapter.Process<IActionDescriptor>("Microsoft.AspNet.Mvc.Abstractions.ActionDescriptor", actionDescriptor);
                    break;
            }

            return typedActionDescriptor;
        }
    }
}
