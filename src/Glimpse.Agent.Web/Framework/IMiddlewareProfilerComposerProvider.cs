using System;
using System.Collections.Generic;

namespace Glimpse.Agent.Web
{
    public interface IMiddlewareProfilerComposerProvider
    {
        IEnumerable<IMiddlewareProfilerComposer> Profilers { get; }
    }
}