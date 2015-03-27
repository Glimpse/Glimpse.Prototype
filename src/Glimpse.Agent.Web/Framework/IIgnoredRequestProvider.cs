using System.Collections.Generic;

namespace Glimpse.Agent.Web
{
    public interface IIgnoredRequestProvider
    {
        IEnumerable<IIgnoredRequestPolicy> Policies { get; }
    }
}