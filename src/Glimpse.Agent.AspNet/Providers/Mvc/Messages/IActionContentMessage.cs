using System.Collections.Generic;

namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public interface IActionContentMessage
    {
        IReadOnlyList<BindingData> Binding { get; }
    }
}