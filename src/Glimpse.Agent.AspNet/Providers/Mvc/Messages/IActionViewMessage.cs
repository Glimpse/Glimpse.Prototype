using System.Collections.Generic;
using Glimpse.Internal;

namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public interface IActionViewMessage
    {
        string ActionId { get; set; }

        string ViewName { get; set; }

        bool DidFind { get; set; }

        IEnumerable<string> SearchedLocations { get; set; }

        string Path { get; set; }

        ViewResult ViewData { get; set; }

        Timing Timing { get; set; }
    }
}