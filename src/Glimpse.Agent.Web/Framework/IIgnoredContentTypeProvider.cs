using System.Collections.Generic;

namespace Glimpse.Agent.Web
{
    public interface IIgnoredContentTypeProvider
    {
        IReadOnlyList<string> ContentTypes { get; }
    }
}