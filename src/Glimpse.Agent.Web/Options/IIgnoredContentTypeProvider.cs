using System.Collections.Generic;

namespace Glimpse.Agent.Web.Options
{
    public interface IIgnoredContentTypeProvider
    {
        IReadOnlyList<IgnoredContentTypeDescriptor> ContentTypes { get; }
    }
}