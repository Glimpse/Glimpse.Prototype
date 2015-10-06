using System;
using System.Collections.Generic;
using Glimpse.Internal;

namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public interface IActionViewMessage
    {
        string ActionId { get; set; }

        string ViewName { get; set; }

        bool ViewDidFind { get; set; }

        IEnumerable<string> ViewSearchedLocations { get; set; }

        string ViewPath { get; set; }

        ViewResult ViewData { get; set; }

        DateTime? ViewStartTime { get; set; }

        DateTime? ViewEndTime { get; set; }

        double ViewDuration { get; set; }
    }
}