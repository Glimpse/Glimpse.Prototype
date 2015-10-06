using System;
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

        public bool ViewDidFind { get; set; }

        public IEnumerable<string> ViewSearchedLocations { get; set; }

        public string ViewPath { get; set; }

        public ViewResult ViewData { get; set; }
        
        public DateTime? ViewStartTime { get; set; }

        public DateTime? ViewEndTime { get; set; }

        public double ViewDuration { get; set; }
    }
}