using System.Collections.Generic;

namespace Glimpse.Server.Web
{
    public interface IResourceProvider
    {
        IEnumerable<IResource> Resources { get; }
    }
}