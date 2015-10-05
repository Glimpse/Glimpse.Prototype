using System.Collections.Generic;

namespace Glimpse.Internal
{
    public interface IMessageIndexProcessor
    {
        IReadOnlyDictionary<string, object> Derive(object payload);
    }
}