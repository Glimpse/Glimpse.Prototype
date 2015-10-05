using System.Collections.Generic;

namespace Glimpse.Internal
{
    public interface IMessageTypeProcessor
    {
        IEnumerable<string> Derive(object payload);
    }
}