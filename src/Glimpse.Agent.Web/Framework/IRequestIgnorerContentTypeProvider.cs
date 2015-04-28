using System.Collections.Generic;

namespace Glimpse.Agent.Web
{
    public interface IRequestIgnorerContentTypeProvider
    {
        IReadOnlyList<string> ContentTypes { get; }
    }
}