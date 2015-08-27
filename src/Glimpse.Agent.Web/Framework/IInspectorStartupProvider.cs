using System;
using System.Collections.Generic;

namespace Glimpse.Agent.Web
{
    public interface IInspectorStartupProvider
    {
        IEnumerable<IInspectorStartup> Startups { get; }
    }
}