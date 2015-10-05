using System.Collections.Generic;

namespace Glimpse.Agent.AspNet
{
    public interface IRequestIgnorerStatusCodeProvider
    {
        IReadOnlyList<int> StatusCodes { get; }
    }
}