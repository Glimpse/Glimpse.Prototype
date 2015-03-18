using System.Collections.Generic;

namespace Glimpse.Agent.Web.Options
{
    public interface IIgnoredStatusCodeProvider
    {
        IReadOnlyList<IgnoredStatusCodeDescriptor> StatusCodes { get; }
    }
}