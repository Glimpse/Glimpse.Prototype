using System.Collections.Generic;
using Glimpse.Internal;

namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public class ViewResultFoundStatusMessage : IActionViewMessage
    {
        public string ActionId { get; set; }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public string ViewName { get; set; }

        public bool DidFind { get; set; }

        public IEnumerable<string> SearchedLocations { get; set; }

        public string Path { get; set; }

        public ViewResult ViewData { get; set; }

        // TODO: Needs to be removed when IActionViewMessage is shifted
        public Timing Timing { get; set; }
    }
}