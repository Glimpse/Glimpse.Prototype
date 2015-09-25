using System;
using System.Collections.Generic;

namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public class ActionResultData
    {
        public string Type { get; set; }

        public object Data { get; set; }

        public class ViewResult
        {
            public int? StatusCode { get; set; }

            public string ViewName { get; set; }

            // TODO: need make sure that these are serializable 
            public IDictionary<string, object> TempData { get; set; }

            // TODO: need make sure that these are serializable 
            public IDictionary<string, object> ViewData { get; set; }

            public string ContentType { get; set; }
        }

        public class ContentResult
        {
            public int? StatusCode { get; set; }

            // TODO: need make sure that these are serializable 
            public string Content { get; set; }

            public string ContentType { get; set; }
        }

        public class ObjectResult
        {
            public int? StatusCode { get; set; }

            // TODO: need make sure that these are serializable 
            public object Value { get; set; }

            public IList<Type> Formatters { get; set; }

            public IList<string> ContentTypes { get; set; }

            public Type DeclaredType { get; set; }
        }
    }
}