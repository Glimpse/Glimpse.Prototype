using System;
using System.Collections.Generic;

namespace Glimpse.Web
{
    public interface IRequestHandlerProvider
    {
        IEnumerable<IRequestHandler> Handlers { get; }
    }
}