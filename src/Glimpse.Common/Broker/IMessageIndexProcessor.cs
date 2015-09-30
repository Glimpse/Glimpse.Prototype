using System.Collections.Generic;

namespace Glimpse
{
    public interface IMessageIndexProcessor
    {
        IReadOnlyDictionary<string, object> Derive(object payload);
    }
}