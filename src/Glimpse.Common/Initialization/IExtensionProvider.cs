using System.Collections.Generic;

namespace Glimpse.Initialization
{
    public interface IExtensionProvider<T>
        where T : class
    {
        IEnumerable<T> Instances { get; }
    }
}
