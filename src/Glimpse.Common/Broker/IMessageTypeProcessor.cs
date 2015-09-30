using System.Collections.Generic;

namespace Glimpse
{
    public interface IMessageTypeProcessor
    {
        IEnumerable<string> Derive(object payload);
    }
}