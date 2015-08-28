using System.Collections.Generic;

namespace Glimpse.Server.Web
{
    public interface IResourceStartupProvider
    {
        IEnumerable<IResourceStartup> Startups { get; }
    }
}