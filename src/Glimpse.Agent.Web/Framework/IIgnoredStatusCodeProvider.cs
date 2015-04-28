using System.Collections.Generic;

namespace Glimpse.Agent.Web
{
    public interface IIgnoredStatusCodeProvider
    {
        IReadOnlyList<int> StatusCodes { get; }
    }
}