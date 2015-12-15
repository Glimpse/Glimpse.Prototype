using System.Collections.Generic;

namespace Glimpse.Services
{
    public interface IExtensionProvider<T>
        where T : class
    {
        IEnumerable<T> Instances { get; }
    }
}
