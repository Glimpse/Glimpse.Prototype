using System.Collections.Generic;

namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public class ViewResult
    {
        // TODO: need make sure that these are serializable 
        public IDictionary<string, object> TempData { get; set; }

        // TODO: need make sure that these are serializable 
        public IDictionary<string, object> ViewData { get; set; }
    }
}