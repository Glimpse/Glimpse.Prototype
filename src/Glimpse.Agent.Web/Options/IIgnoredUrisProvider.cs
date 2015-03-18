using System.Collections.Generic;

namespace Glimpse.Agent.Web.Options
{
    public interface IIgnoredUrisProvider
    {
        IReadOnlyList<IgnoredUrisDescriptor> IgnoredUris { get; }
    }
}