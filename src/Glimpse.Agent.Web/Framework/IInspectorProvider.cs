using System.Collections.Generic;

namespace Glimpse.Agent.Web
{
    public interface IInspectorProvider
    {
        IEnumerable<IInspector> Inspectors { get; }
    }
}