using System.Collections.Generic;

namespace Glimpse.Server.Web
{
    public interface IMiddlewareResourceComposerProvider
    {
        IEnumerable<IMiddlewareResourceComposer> Resources { get; }
    }
}