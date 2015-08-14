using System;
using System.Collections.Generic;

namespace Glimpse.Web
{
    public interface IMiddlewareResourceComposerProvider
    {
        IEnumerable<IMiddlewareResourceComposer> Resources { get; }
    }
}