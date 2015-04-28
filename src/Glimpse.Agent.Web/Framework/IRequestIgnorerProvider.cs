using System.Collections.Generic;

namespace Glimpse.Agent.Web
{
    public interface IRequestIgnorerProvider
    {
        IEnumerable<IRequestIgnorer> Policies { get; }
    }
}