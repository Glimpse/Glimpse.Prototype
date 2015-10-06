using System.Collections.Generic;

namespace Glimpse.Agent
{
    public interface IRequestIgnorerStatusCodeProvider
    {
        IReadOnlyList<int> StatusCodes { get; }
    }
}