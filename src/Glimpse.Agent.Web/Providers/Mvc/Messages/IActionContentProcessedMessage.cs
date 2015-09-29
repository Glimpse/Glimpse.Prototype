using System.Collections.Generic;

namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public interface IActionContentProcessedMessage
    {
        IReadOnlyList<BindingData> Binding { get; }
    }
}