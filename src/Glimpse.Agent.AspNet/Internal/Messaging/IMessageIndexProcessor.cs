using System.Collections.Generic;

namespace Glimpse.Agent.Internal.Messaging
{
    public interface IMessageIndexProcessor
    {
        IReadOnlyDictionary<string, object> Derive(object payload);
    }
}