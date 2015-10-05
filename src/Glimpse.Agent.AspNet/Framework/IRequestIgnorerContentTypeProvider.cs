using System.Collections.Generic;

namespace Glimpse.Agent.AspNet
{
    public interface IRequestIgnorerContentTypeProvider
    {
        IReadOnlyList<string> ContentTypes { get; }
    }
}