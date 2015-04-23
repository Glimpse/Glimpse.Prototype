using System;
using System.Collections.Generic;

namespace Glimpse.Agent.Web
{
    public interface IRequestProfilerProvider
    {
        IEnumerable<IRequestProfiler> Profilers { get; }
    }
}