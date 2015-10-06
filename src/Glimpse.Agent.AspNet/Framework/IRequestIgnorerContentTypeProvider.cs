using System.Collections.Generic;

namespace Glimpse.Agent
{
    public interface IRequestIgnorerContentTypeProvider
    {
        IReadOnlyList<string> ContentTypes { get; }
    }
}