using System;
using System.Collections.Generic;

namespace Glimpse.Web
{
    public interface IRequestRuntimeProvider
    {
        IEnumerable<IRequestRuntime> Runtimes { get; }
    }
}