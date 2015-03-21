using System.Collections.Generic;

namespace Glimpse.Agent.Web.Options
{
    public interface IIgnoredRequestProvider
    {
        IEnumerable<IIgnoredRequestPolicy> Policies { get; }
    }
}