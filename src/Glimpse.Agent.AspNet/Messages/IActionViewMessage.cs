using System;
using System.Collections.Generic;

namespace Glimpse.Agent.Messages
{
    public interface IActionViewMessage
    {
        string ActionId { get; set; }

        string ViewName { get; set; }

        bool ViewDidFind { get; set; }

        IEnumerable<string> ViewSearchedLocations { get; set; }

        string ViewPath { get; set; }

        ViewResultData ViewData { get; set; }

        DateTime? ViewStartTime { get; set; }

        DateTime? ViewEndTime { get; set; }

        double ViewDuration { get; set; }
    }
}