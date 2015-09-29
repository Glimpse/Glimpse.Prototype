using System.Collections.Generic;

namespace Glimpse
{
    public interface IExtensionProvider<T>
        where T : class
    {
        IEnumerable<T> Instances { get; }
    }
}
