using System.Collections.Generic;

namespace Glimpse.Agent.Internal.Messaging
{
    public interface IMessageTypeProcessor
    {
        IEnumerable<string> Derive(object payload);
    }
}