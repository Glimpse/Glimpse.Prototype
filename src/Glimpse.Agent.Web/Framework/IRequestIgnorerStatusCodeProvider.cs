using System.Collections.Generic;

namespace Glimpse.Agent.Web
{
    public interface IRequestIgnorerStatusCodeProvider
    {
        IReadOnlyList<int> StatusCodes { get; }
    }
}