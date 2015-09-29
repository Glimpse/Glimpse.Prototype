using System.Collections.Generic;

namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public interface IActionViewFoundMessage
    {
        string ActionId { get; set; }

        string ViewName { get; set; }

        bool DidFind { get; set; }
        
        ViewResult ViewData { get; set; }
    }
}