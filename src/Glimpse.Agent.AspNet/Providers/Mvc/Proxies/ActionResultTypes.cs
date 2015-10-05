using System;
using System.Collections.Generic;

namespace Glimpse.Agent.AspNet.Mvc.Proxies
{
    public class ActionResultTypes
    {
        public interface IViewResult
        {
            int? StatusCode { get; }
            string ViewName { get; }
            IDictionary<string, object> TempData { get; }
            IDictionary<string, object> ViewData { get; }
            object ContentType { get; }
        }

        public interface IContentResult
        {
            int? StatusCode { get; }
            string Content { get; }
            object ContentType { get; }
        }

        public interface IObjectResult
        {
            int? StatusCode { get; set; }

            object Value { get; set; }

            IList<object> Formatters { get; set; }

            IList<string> ContentTypes { get; set; }

            Type DeclaredType { get; set; }
        }
    }
}